
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    #region 상태 변수
    private IState<PlayerCtrl> m_idle_state;
    private IState<PlayerCtrl> m_move_state;
    private PlayerStateContext m_state_context;

    #endregion
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public bool NonPass { get; set; }
    public float Speed { get; set; } = 0.02f;

    private Vector3 m_direction = Vector3.zero;
    public Vector3 Direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }

    public PlayerStateContext StateContext
    {
        get { return m_state_context; }
        private set { m_state_context = value; }
    }

    public LayerMask m_layer_mask;

    private int m_walk_count;
    public int WalkCount
    {
        get { return m_walk_count; }
        set { m_walk_count = value; }
    }

    private int m_current_walk_count;
    public int CurrentWalkCount
    {
        get { return m_current_walk_count; }
        set { m_current_walk_count = value; }
    }


    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.Playing, GameManager.Instance.Playing);
        GameEventBus.Subscribe(GameEventType.Setting, GameManager.Instance.Setting);
        GameEventBus.Subscribe(GameEventType.Dead, GameManager.Instance.Dead);
        GameEventBus.Subscribe(GameEventType.Clear, GameManager.Instance.Clear);

        GameEventBus.Publish(GameEventType.Playing);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.Playing, GameManager.Instance.Playing);
        GameEventBus.Unsubscribe(GameEventType.Setting, GameManager.Instance.Setting);
        GameEventBus.Unsubscribe(GameEventType.Dead, GameManager.Instance.Dead);

        DataManager.Instance.Current = -1;
    }

    private void Awake()
    {
        transform.position = DataManager.Instance.PlayerData.m_player_position;

        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<BoxCollider2D>();

        m_idle_state = gameObject.AddComponent<PlayerIdleState>();
        m_move_state = gameObject.AddComponent<PlayerMoveState>();

        StateContext = new PlayerStateContext(this);

        ChangeState(PlayerState.Idle);
    }

    private void Update()
    {
        DataManager.Instance.PlayerData.m_player_position = transform.position;

        Direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        StateContext.ExecuteUpdate();
    }

    public void ChangeState(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Idle:
                StateContext.Transition(m_idle_state);
                break;
            
            case PlayerState.Move:
                StateContext.Transition(m_move_state);
                break;
        }
    }
}