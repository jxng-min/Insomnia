using UnityEngine;

[System.Serializable]
public class InvestigationQuest : QuestBase
{
    [Header("조사해야 하는 오브젝트의 ID")]
    [SerializeField] private ObjectCode m_target_object_code;
    public ObjectCode ObjectCode
    {
        get { return m_target_object_code; }
    }

    [Header("조사할 횟수(기본적으로 1을 입력)")]
    [SerializeField] private int m_total_count;
    public int InvestigationCount
    {
        get { return m_total_count; }
    }

    private int m_current_count = 0;
    public int CurrentCount
    {
        get { return m_current_count; }
    }

    public override string GetProgressText()
    {
        return $"{CurrentCount} / {InvestigationCount}";
    }
}