using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : Singleton<QuestUIManager>
{
    private RectTransform m_compact_quest_root; 

    [Header("컴팩트 퀘스트 UI 프리펩")]
    [SerializeField] private GameObject m_compact_ui_prefab;

    private Dictionary<int, QuestContentData> m_quest_contents = new Dictionary<int, QuestContentData>();
    private Dictionary<int, QuestCompactContent> m_compact_quest_contents = new Dictionary<int, QuestCompactContent>();

    public void Initialization()
    {
        m_compact_quest_root = GameObject.Find("Compact Quest Root").GetComponent<RectTransform>();

        m_quest_contents.Clear();
        m_compact_quest_contents.Clear();

        foreach(var content_data in QuestManager.Instance.QuestContentList.DataList)
        {
            m_quest_contents.Add(content_data.m_quest_id, content_data);
        }
    }

    public void CompleteQuest(QuestData quest_data)
    {
        ToggleCompactQuestContent(quest_data ,false);
    }

    public void ToggleCompactQuestContent(QuestData quest_data, bool is_enable)
    {
        if(is_enable)
        {
            if(m_compact_quest_contents.ContainsKey(quest_data.ID))
            {
                Debug.LogErrorFormat(
                    "{0} 퀘스트가 중복됩니다."
                    , quest_data.ID
                );
            }
            else
            {
                QuestCompactContent new_quest_content = Instantiate(m_compact_ui_prefab, Vector3.zero, Quaternion.identity, m_compact_quest_root).GetComponent<QuestCompactContent>();

                new_quest_content.Init(quest_data);
                m_compact_quest_contents.Add(quest_data.ID, new_quest_content);
                new_quest_content.UpdateCompactQuestContents(m_quest_contents[quest_data.ID]);
            }
        }
        else
        {
            if(m_compact_quest_contents.ContainsKey(quest_data.ID))
            {
                Destroy(m_compact_quest_contents[quest_data.ID].gameObject);
                m_compact_quest_contents.Remove(quest_data.ID);
            }
        }

        StartCoroutine(RefreshQuestCompactLayout());
    }

    public void UpdateCurrentQuestState(int quest_id)
    {
        if(m_compact_quest_contents.ContainsKey(quest_id))
        {
            m_compact_quest_contents[quest_id].UpdateCompactQuestContents(m_quest_contents[quest_id]);
        }
    }

    private IEnumerator RefreshQuestCompactLayout()
    {
        var compact_root = m_compact_quest_root.GetComponent<VerticalLayoutGroup>();

        compact_root.reverseArrangement = true;

        yield return null;

        compact_root.reverseArrangement = false;
    }
}
