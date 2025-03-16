using UnityEngine;

[System.Serializable]
public class QuestContentData
{
    public int m_quest_id;
    [TextArea] public string m_title;
    [TextArea] public string m_compact_content;
}

[System.Serializable]
public class QuestContentDataList
{
    [SerializeField] private QuestContentData[] m_data_list;
    public QuestContentData[] DataList
    {
        get { return m_data_list; }
    }
}
