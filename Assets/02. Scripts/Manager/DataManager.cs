using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Singleton;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    public PlayerData m_now_player = new PlayerData();
    public string m_save_path;
    public int m_now_slot;

    private void Start()
    {
        m_save_path = Application.persistentDataPath + "/save";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(m_now_player);
        File.WriteAllText(m_save_path + m_now_slot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(m_save_path + m_now_slot.ToString());
        m_now_player = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        m_now_slot = -1;
        m_now_player = new PlayerData();
    }
}
