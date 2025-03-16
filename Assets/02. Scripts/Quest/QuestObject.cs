using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [Header("이 오브젝트가 가질 퀘스트 목록")]
    [SerializeField] QuestData[] m_quest_data_list;

    public bool IsExistQuest(out int quest_id)
    {
        quest_id = -1;

        foreach(var quest_data in m_quest_data_list)
        {
            if(QuestManager.Instance.CheckQuestState(quest_data.ID) == QuestState.CLEARED_PAST)
            {
                continue;
            }

            for(int i = 0; i < quest_data.PrerequisteQuestIDs.Length; i++)
            {
                if(QuestManager.Instance.CheckQuestState(quest_data.PrerequisteQuestIDs[i]) is not QuestState.CLEARED_PAST)
                {
                    return false;
                }
            }

            quest_id = quest_data.ID;
            return true;
        }

        return false;
    }
}
