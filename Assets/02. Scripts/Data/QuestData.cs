using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string m_quest_name;
    public int[] m_npc_id;

    public QuestData(string name, int[] npc)
    {
        m_quest_name = name;
        m_npc_id = npc;
    }
}
