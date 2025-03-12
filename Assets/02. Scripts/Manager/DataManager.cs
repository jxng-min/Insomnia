using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    private PlayerData m_now_player;
    public PlayerData PlayerData
    {
        get { return m_now_player; }
        set { m_now_player = value; }
    }

    private string m_save_data_path;
    public string DataPath
    {
        get { return m_save_data_path; }
    }

    private int m_now_slot;
    public int Current
    {
        get { return m_now_slot; }
        set { m_now_slot = value; }
    }

    private new void Awake()
    {
        base.Awake();

        m_save_data_path = Path.Combine(Application.persistentDataPath, "save");

        PlayerData = new PlayerData();
    }

    public void SaveData()
    {
        var json_data = JsonUtility.ToJson(PlayerData);
        File.WriteAllText(m_save_data_path + m_now_slot.ToString(), json_data);
    }

    public void LoadData()
    {
        var json_data = File.ReadAllText(m_save_data_path + m_now_slot.ToString());
        PlayerData = JsonUtility.FromJson<PlayerData>(json_data);
    }

    public void DataClear()
    {
        m_now_slot = -1;
        PlayerData = null;
    }
}
