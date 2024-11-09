using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _EventBus;
using System;

public class TimerCtrl : MonoBehaviour
{
    private float m_sec_cnt;
    private int m_min;
    private int m_sec;

    void Start()
    {
        m_min = Mathf.FloorToInt(DataManager.Instance.m_now_player.m_play_time / 60f);
        m_sec = Mathf.FloorToInt(DataManager.Instance.m_now_player.m_play_time % 60f);
    }

    void Update()
    {
        if(!GameManager.Instance.m_is_talk && GameManager.Instance.m_game_status == "playing")
        {
            DataManager.Instance.m_now_player.m_play_time += Time.deltaTime;

            m_min = Mathf.FloorToInt(DataManager.Instance.m_now_player.m_play_time / 60f);
            m_sec = Mathf.FloorToInt(DataManager.Instance.m_now_player.m_play_time % 60f);
        }
    }
}
