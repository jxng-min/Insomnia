using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameEventType m_current_game_state;
    public GameEventType GameState
    {
        get { return m_current_game_state; }
        private set { m_current_game_state = value; }
    }

    private PlayerCtrl m_player_ctrl;
    public PlayerCtrl Player
    {
        get { return m_player_ctrl; }
        private set { m_player_ctrl = value; }
    }

    private bool m_can_init = true;

    private int ending_type = 0;
    public int EndingType
    {
        get { return ending_type; }
        set { ending_type = value; }
    }

    private new void Awake()
    {
        base.Awake();

        GameEventBus.Subscribe(GameEventType.None, None);
        GameEventBus.Subscribe(GameEventType.Loading, Loading);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void None()
    {
        GameState = GameEventType.None;

        SoundManager.Instance.PlayBGM("Title Background");
        QuestManager.Instance.ResetScriptableObject();
        EndingType = 0;

        m_can_init = true;
    }

    public void Loading()
    {
        GameState = GameEventType.Loading;
    }

    public void Playing()
    {
        GameState = GameEventType.Playing;

        if(m_can_init)
        {
            m_can_init = false;

            Player = FindAnyObjectByType<PlayerCtrl>();

            SoundManager.Instance.PlayBGM("Game Background");
            QuestManager.Instance.Initialization();
            QuestUIManager.Instance.Initialization();
            QuestManager.Instance.LoadCurrentQuestData();
        }
        else
        {
            DialogueManager.Instance.Initialization();

            SoundManager.Instance.BGM.Play();
        }
    }

    public void Setting()
    {
        GameState = GameEventType.Setting;
        SoundManager.Instance.BGM.Pause();
    }

    public void Dead()
    {
        GameState = GameEventType.Dead;
    }

    public void Clear()
    {
        GameState = GameEventType.Dead;
    }
}