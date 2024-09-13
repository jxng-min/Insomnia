using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class LoadCtrl : MonoBehaviour
{
    public GameObject m_save_panel;
    public TMP_Text[] m_slot_text;
    bool[] m_save_files = new bool[5] {false, false, false, false, false};

    public void SetSlotText(int slot_number)
    {
        DataManager.Instance.m_now_slot = slot_number;

        if(m_save_files[slot_number])
            DataManager.Instance.LoadData();

        StartSaveFile();
    }

    void Start()
    {
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
        DataManager.Instance.DataClear(); 
    }

    public void OpenLoadPanel()
    {
        m_save_panel.SetActive(true);
    }

    public void StartSaveFile()
    {
        if(!m_save_files[DataManager.Instance.m_now_slot])
            DataManager.Instance.m_now_player = new PlayerData();

        SceneManager.LoadScene("Game");
    }
}
