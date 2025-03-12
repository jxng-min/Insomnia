using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector2 m_player_position;
    public float m_play_time;
    public int m_quest_id;
    public int m_quest_action_id;
    public List<string> m_items;

    public PlayerData()
    {
        m_player_position = new Vector2(12.75f, -20.25f);
        m_play_time = 0f;
        m_quest_id = 10;
        m_quest_action_id = 0;
        m_items = new List<string>();
    }
}
