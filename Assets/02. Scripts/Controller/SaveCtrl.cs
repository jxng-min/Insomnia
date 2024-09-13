using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SaveCtrl : MonoBehaviour
{
    public GameObject m_save_panel;
    public TMP_Text[] m_slot_text;
    bool[] m_save_files = new bool[5];

    private void OnEnable()
    {
        ShowSlot();
    }

    public void SaveFile(int slot_number)
    {
        int current_slot_number = DataManager.Instance.m_now_slot;
        DataManager.Instance.m_now_slot = slot_number;

        DataManager.Instance.SaveData();
        DataManager.Instance.m_now_slot = current_slot_number;

        ShowSlot();
    }

    private void ShowSlot()
    {
        PlayerData current_player_data = DataManager.Instance.m_now_player;
        for(int i = 0; i <= 3; i++)
        {
            if(File.Exists(DataManager.Instance.m_save_path + $"{i}"))
            {
                m_save_files[i] = true;

                DataManager.Instance.m_now_slot = i;
                DataManager.Instance.LoadData();

                int play_min = Mathf.FloorToInt(DataManager.Instance.m_now_player.m_play_time / 60f);
                int play_sec = Mathf.FloorToInt(DataManager.Instance.m_now_player.m_play_time % 60f);
                m_slot_text[i].text = "[시간] " + play_min.ToString() + " : " + play_sec.ToString();
            }
            else
                m_slot_text[i].text = "비어있음";
        }
        DataManager.Instance.m_now_player = current_player_data;     
    }
}
