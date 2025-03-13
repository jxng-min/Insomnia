using UnityEngine;
using System.IO;

public class TitleCtrl : MonoBehaviour
{
    [Header("게임 데이터 로드 UI")]
    [SerializeField] private GameObject m_load_ui_object;
    private Animator m_load_ui_animator;
    private LoadSlotCtrl m_load_ui_button_ctrl;

    private bool[] m_save_files = new bool[4];

    private void Awake()
    {
        m_load_ui_animator = m_load_ui_object.GetComponent<Animator>();
        m_load_ui_button_ctrl = m_load_ui_object.GetComponent<LoadSlotCtrl>();   
    }

    private void Start()
    {
        GameEventBus.Publish(GameEventType.None);

        for(int i = 0; i <= 3; i++)
        {
            m_load_ui_button_ctrl.Slots[i].Title.text = $"[슬롯 {i + 1}]";

            if(File.Exists(DataManager.Instance.DataPath + $"{i}"))
            {
                m_save_files[i] = true;

                DataManager.Instance.Current = i;
                DataManager.Instance.LoadData();

                int play_min = Mathf.FloorToInt(DataManager.Instance.PlayerData.m_play_time / 60f);
                int play_sec = Mathf.FloorToInt(DataManager.Instance.PlayerData.m_play_time % 60f);
                
                m_load_ui_button_ctrl.Slots[i].Time.text = $"플레이 타임: {play_min} : {play_sec}";
            }
            else
            {
                m_load_ui_button_ctrl.Slots[i].Time.text = "비어있음";
                m_load_ui_button_ctrl.Slots[i].Button.interactable = false;
            }
        }

        DataManager.Instance.DataClear(); 
    }

    public void BTN_Start()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        DataManager.Instance.PlayerData = new PlayerData();
        LoadingManager.Instance.LoadScene("Game");
    }

    public void BTN_Load()
    {
        SoundManager.Instance.PlayEffect("Button Click");
        m_load_ui_animator.SetBool("Open", true);
        m_load_ui_button_ctrl.Initialization();
    }

    public void BTN_Exit()
    {
        SoundManager.Instance.PlayEffect("Button Click");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BTN_LoadSlot(int slot_number)
    {
        SoundManager.Instance.PlayEffect("Button Click");
        DataManager.Instance.Current = slot_number;

        if(m_save_files[slot_number])
        {
            DataManager.Instance.LoadData();
        }

        LoadingManager.Instance.LoadScene("Game");
    }
}
