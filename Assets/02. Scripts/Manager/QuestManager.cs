using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestManager : Singleton<QuestManager>
{
    private Inventory m_inventory;

    [Header("에디터에서 로드한 퀘스트의 목록")]
    [SerializeField] private List<QuestData> m_preloaded_quests;

    [Header("현재까지 로드된 총 퀘스트의 개수(절대 수정 X)")]
    [field: SerializeField] public int QuestCount { get; private set; } = -1;

    private Dictionary<int, QuestData> m_quests = new Dictionary<int, QuestData>();
    public Dictionary<int, QuestData> Quests
    {
        get { return m_quests; }
    }

    private List<QuestData> m_received_quests = new List<QuestData>();
    public List<QuestData> ReceivedQuests
    {
        get { return m_received_quests; }
    }

    private string m_all_quest_data_path;
    private string m_current_quest_data_path;

    private QuestContentDataList m_quest_content_list;
    public QuestContentDataList QuestContentList
    {
        get { return m_quest_content_list; }
        private set { m_quest_content_list = value; }
    }

    private new void Awake()
    {
        base.Awake();

        m_all_quest_data_path = Path.Combine(Application.streamingAssetsPath, "QuestData.json");

        LoadAllQuestData();
        LoadPreloadQuests();
    }

    public void Initialization()
    {
        m_inventory = GameObject.Find("Inventory Manager").GetComponent<Inventory>();
        m_inventory.LoadInventory();

        m_current_quest_data_path = Path.Combine(Application.persistentDataPath, "Quest") + DataManager.Instance.Current.ToString() + ".json";
    }

    public void ResetScriptableObject()
    {
        for(int id = 0; id < Quests.Count; id++)
        {
            Quests[id].QuestState = QuestState.NEVER_RECEIVED;

            foreach(var quest in Quests[id].All)
            {
                if(quest is InvestigationQuest)
                {
                    (quest as InvestigationQuest).CurrentCount = 0;
                }
                else
                {
                    (quest as ItemQuest).CurrentCount = 0;
                }

                quest.ParticularClear = false;
            }
        }
    }

    private void LoadAllQuestData()
    {
        if(File.Exists(m_all_quest_data_path))
        {
            var json_data = File.ReadAllText(m_all_quest_data_path);
            QuestContentList = JsonUtility.FromJson<QuestContentDataList>(json_data);

        }
        else
        {
            Debug.Log($"{m_all_quest_data_path}가 존재하지 않습니다.");
        }
    }

    private void LoadPreloadQuests()
    {
        foreach(var quest_data in m_preloaded_quests)
        {
            var new_quest = quest_data;

            List<QuestBase> all_quests = new List<QuestBase>();
            foreach(var investigation_quest in new_quest.InvestigationQuests)
            {
                all_quests.Add(investigation_quest);
            }

            foreach(var item_quest in new_quest.ItemQuests)
            {
                all_quests.Add(item_quest);
            }

            new_quest.All = all_quests.ToArray();

            Quests.Add(quest_data.ID, new_quest);
        }
    }

    public void LoadCurrentQuestData()
    {
        if(File.Exists(m_current_quest_data_path))
        {
            LoadCurrentQuests();
        }
        else
        {
            Debug.Log($"{m_current_quest_data_path}가 존재하지 않습니다.");
        }
    }

    private void LoadCurrentQuests()
    {
        var json_data = File.ReadAllText(m_current_quest_data_path);
        var quest_data_list = JsonUtility.FromJson<SavedQuestList>(json_data);

        foreach(var quest_data in quest_data_list.QuestList)
        {
            var loaded_quest = Quests[quest_data.ID];

            loaded_quest.InvestigationQuests = loaded_quest.InvestigationQuests;
            loaded_quest.ItemQuests = loaded_quest.ItemQuests;

            List<QuestBase> all_quests = new List<QuestBase>();
            foreach(var investigation_quest in loaded_quest.InvestigationQuests)
            {
                all_quests.Add(investigation_quest);
            }

            foreach(var item_quest in loaded_quest.ItemQuests)
            {
                all_quests.Add(item_quest);
            }

            loaded_quest.All = all_quests.ToArray();
            loaded_quest.QuestState = quest_data.QuestState;

            ReceiveQuest(loaded_quest.ID);
        }
    }

    public void SaveCurrentQuests(int index)
    {
        SavedQuestList quest_data_list = new SavedQuestList();

        List<QuestSaveData> save_data_list = new List<QuestSaveData>();
        foreach(var quest in Quests.Values)
        {
            if(quest.QuestState != QuestState.NEVER_RECEIVED)
            {
                QuestSaveData save_data = new QuestSaveData
                {
                    ID = quest.ID,
                    InvestigationQuests = quest.InvestigationQuests,
                    ItemQuests = quest.ItemQuests,
                    QuestState = quest.QuestState
                };

                save_data_list.Add(save_data);
            }
        }

        quest_data_list.QuestList = save_data_list.ToArray();

        string json_data = JsonUtility.ToJson(quest_data_list, true);

        string save_path = Path.Combine(Application.persistentDataPath, "Quest") + index.ToString() + ".json";
        File.WriteAllText(save_path, json_data);
    }

    public void ReceiveQuest(int quest_id)
    {
        QuestData quest_data = Quests[quest_id];

        QuestUIManager.Instance.ToggleCompactQuestContent(quest_data, true);

        m_received_quests.Add(quest_data);

        if(quest_data.QuestState == QuestState.CLEARED_PAST)
        {
            CompleteQuest(quest_data.ID, false);
        }
        else
        {
            quest_data.QuestState = QuestState.ON_GOING;
        }

        UpdateItemQuestCount();
        for(int i = 0; i < quest_data.InvestigationQuests.Length; i++)
        {
            UpdateInvestigationQuestCount(quest_data.InvestigationQuests[i].ObjectCode);
        }           
    }

    public void CompleteQuest(int quest_id, bool is_give_reward = true)
    {
        QuestData quest_data = Quests[quest_id];

        if(is_give_reward)
        {
            for(int i = 0; i < quest_data.Items.Length; i++)
            {
                m_inventory.AcquireItem(quest_data.Items[i], quest_data.ItemCounts[i]);
            }
            
            UpdateItemQuestCount();
        }

        QuestUIManager.Instance.CompleteQuest(quest_data);
        m_received_quests.Remove(quest_data);
        quest_data.QuestState = QuestState.CLEARED_PAST;

        if(quest_id < 11)
        {
            ReceiveQuest(quest_id + 1);
        }
        else if(quest_id == 11)
        {
            ReceiveQuest(quest_id + 1);
            ReceiveQuest(quest_id + 2);
        }
        else
        {
            if(quest_id == 12)
            {
                GameManager.Instance.EndingType = 1;
            }
            else
            {
                GameManager.Instance.EndingType = 2;
            }

            LoadingManager.Instance.LoadScene("Epilogue");
        }
    }

    public QuestState CheckQuestState(int quest_id)
    {
        if(Quests[quest_id].QuestState == QuestState.CLEARED_PAST)
        {
            return QuestState.CLEARED_PAST;
        }

        foreach(var quest_data in m_received_quests)
        {
            if(quest_data.ID == quest_id)
            {
                foreach(var quest in quest_data.All)
                {
                    if(quest.ParticularClear is false)
                    {
                        return QuestState.ON_GOING;
                    }
                }

                return QuestState.CLEAR;
            }
        }

        return QuestState.NEVER_RECEIVED;
    }

    public void UpdateInvestigationQuestCount(ObjectCode object_code)
    {
        for(int i = 0; i < m_received_quests.Count; i++)
        {
            for(int j = 0; j  < m_received_quests[i].InvestigationQuests.Length; j++)
            {
                if(m_received_quests[i].InvestigationQuests[j].ObjectCode == object_code)
                {
                    ++m_received_quests[i].InvestigationQuests[j].CurrentCount;

                    m_received_quests[i].InvestigationQuests[j].ParticularClear
                        = m_received_quests[i].InvestigationQuests[j].CurrentCount >= m_received_quests[i].InvestigationQuests[j].InvestigationCount;

                    return;
                }
            }
        }
    }

    public void UpdateItemQuestCount()
    {
        foreach(var quest_data in m_received_quests)
        {
            foreach(var item_quest in quest_data.ItemQuests)
            {
                item_quest.CurrentCount = m_inventory.GetItemCount(item_quest.ItemCode);
                Debug.Log($"아이템 코드{item_quest.ItemCode} : 아이템 개수{m_inventory.GetItemCount(item_quest.ItemCode)}");

                item_quest.ParticularClear = item_quest.CurrentCount >= item_quest.TotalCount;
            }
        }
    }

    #region Editor Method
#if UNITY_EDITOR
    public void LoadQuests(List<QuestData> all_quests)
    {
        m_preloaded_quests = new List<QuestData>();
        m_preloaded_quests = all_quests;

        QuestCount = all_quests is null ? -1 : all_quests.Count;
    }
#endif
    #endregion
}

#region Editor Function
#if UNITY_EDITOR
[CustomEditor(typeof(QuestManager))]
public class QuestManager_EditorFunctions : Editor
{
    private QuestManager m_base_target;

    private void OnEnable()
    {
        m_base_target = (QuestManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n모든 퀘스트 불러오기");

        if(GUILayout.Button("불러오기"))
        {
            LoadToArray();
        }
    }

    private void LoadToArray()
    {
        bool is_duplicated = false;

        string[] guid_array = AssetDatabase.FindAssets("t:QuestData");

        List<QuestData> quests = new List<QuestData>();
        Dictionary<int, QuestData> quest_duplicate = new Dictionary<int, QuestData>();

        foreach(string guid in guid_array)
        {
            var asset_path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<QuestData>(asset_path);

            if(quest_duplicate.ContainsKey(asset.ID))
            {
                Debug.LogErrorFormat(
                    "{0}와 {1}가 퀘스트 ID {2}로 겹칩니다!"
                    , quest_duplicate[asset.ID].name
                    , asset.name
                    , asset.ID);
                
                is_duplicated = true;
                
                break;
            }

            quest_duplicate.Add(asset.ID, asset);
            quests.Add(asset);
        }

        if(!is_duplicated)
        {
            Debug.LogFormat(
                "<color=cyan>{0}개의 퀘스트가 중복 없이 로드되었습니다.</color>"
                , quest_duplicate.Count);
        }
        else
        {
            quest_duplicate.Clear();
            quest_duplicate = null;
            quests.Clear();
            quests = null;
        }

        m_base_target.LoadQuests(quests);
    }
}
#endif
#endregion