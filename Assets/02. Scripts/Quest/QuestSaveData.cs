using UnityEngine;

[System.Serializable]
public class QuestSaveData
{
    [Header("퀘스트의 고유한 ID")]
    [SerializeField] private int m_quest_id;
    public int ID
    {
        get { return m_quest_id; }
        set { m_quest_id = value; }
    }

    [Header("서브 조사 퀘스트의 목록")]
    [SerializeField] private InvestigationQuest[] m_investigation_quests;
    public InvestigationQuest[] InvestigationQuests
    {
        get { return m_investigation_quests; }
        set { m_investigation_quests = value; }
    }

    [Header("서브 아이템 퀘스트의 목록")]
    [SerializeField] private ItemQuest[] m_item_quests;
    public ItemQuest[] ItemQuests
    {
        get { return m_item_quests; }
        set { m_item_quests = value; }
    }

    [Header("현재 퀘스트의 상태")]
    [SerializeField] private QuestState m_current_quest_state;
    public QuestState QuestState
    {
        get { return m_current_quest_state; }
        set { m_current_quest_state = value; }
    }
}

[System.Serializable]
public class SavedQuestList
{
    [SerializeField] QuestSaveData[] m_quest_save_data;
    public QuestSaveData[] QuestList
    {
        get { return m_quest_save_data; }
        set { m_quest_save_data = value; }
    }
}