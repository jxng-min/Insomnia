using System.IO;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private static bool m_is_ui_active = false;
    public static bool IsActive
    {
        get { return m_is_ui_active; }
        private set { m_is_ui_active = value; }
    }

    [Header("설정 UI 오브젝트")]
    [SerializeField] private GameObject m_setting_ui_object;
    private ButtonCtrl m_setting_button_ctrl;

    [Header("세이브 UI 애니메이터")]
    [SerializeField] private Animator m_save_ui_animator;
    private LoadSlotCtrl m_save_button_ctrl;

    [Header("인벤토리 UI 애니메이터")]
    [SerializeField] private Animator m_inventory_ui_animator;

    private void Awake()
    {
        m_save_button_ctrl = m_save_ui_animator.GetComponent<LoadSlotCtrl>();
        m_setting_button_ctrl = m_setting_ui_object.GetComponent<ButtonCtrl>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_save_ui_animator.GetBool("Open"))
            {
                m_setting_button_ctrl.enabled = true;
                m_setting_button_ctrl.Initialization();
                m_save_ui_animator.SetBool("Open", false);
            }
            else if(m_inventory_ui_animator.GetBool("Open"))
            {
                m_inventory_ui_animator.SetBool("Open", false);
            }
            else
            {
                if(!IsActive)
                {
                    GameEventBus.Publish(GameEventType.Setting);

                    IsActive = true;
                    m_setting_ui_object.SetActive(true);
                }
                else
                {
                    GameEventBus.Publish(GameEventType.Playing);

                    IsActive = false;
                    m_setting_ui_object.SetActive(false);
                }
            }
        }   
    }

    private void LoadData()
    {
        for(int i = 0; i <= 3; i++)
        {
            m_save_button_ctrl.Slots[i].Title.text = $"[슬롯 {i + 1}]";

            string data_path = DataManager.Instance.DataPath + $"{i}";

            if(File.Exists(data_path))
            {
                var json_data = File.ReadAllText(data_path);
                var player_data = JsonUtility.FromJson<PlayerData>(json_data);

                int min = Mathf.FloorToInt(player_data.m_play_time / 60);
                int sec = Mathf.FloorToInt(player_data.m_play_time % 60);

                m_save_button_ctrl.Slots[i].Time.text = $"플레이 타임: {min} : {sec}";
            }
            else
            {
                m_save_button_ctrl.Slots[i].Time.text = "비어있음";
            }
        }
    }

    public void BTN_Save()
    {
        m_setting_button_ctrl.enabled = false;
        m_save_ui_animator.SetBool("Open", true);
        m_save_button_ctrl.Initialization();
        LoadData();
    }

    public void BTN_Inventory()
    {
        m_setting_button_ctrl.enabled = false;
        m_inventory_ui_animator.SetBool("Open", true);
    }

    public void BTN_Title()
    {
        LoadingManager.Instance.LoadScene("Title");
    }

    public void BTN_Slot(int index)
    {
        string save_path = DataManager.Instance.DataPath + $"{index}";

        var json_data = JsonUtility.ToJson(DataManager.Instance.PlayerData);
        File.WriteAllText(save_path, json_data);

        int min = Mathf.FloorToInt(DataManager.Instance.PlayerData.m_play_time / 60);
        int sec = Mathf.FloorToInt(DataManager.Instance.PlayerData.m_play_time % 60);

        m_save_button_ctrl.Slots[index].Time.text = $"플레이 타임: {min} : {sec}";
    }
}
