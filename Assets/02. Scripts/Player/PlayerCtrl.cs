using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    #region 상태 변수

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

    public LayerMask m_layer_mask;

    [SerializeField] private int m_walk_count;
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

    private bool m_is_move = false;
    public bool IsMove
    {
        get { return m_is_move; }
        set { m_is_move = value; }
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
    }

    private void Update()
    {
        if(GameManager.Instance.GameState == GameEventType.Playing)
        {
            DataManager.Instance.PlayerData.m_player_position = transform.position;

            if(!IsMove)
            {
                if(Input.GetAxisRaw("Horizontal") is not 0f || Input.GetAxisRaw("Vertical") is not 0f)
                {
                    StartCoroutine(MoveCoroutine());
                }
            }
        }
    }

    private IEnumerator MoveCoroutine()
    {
        IsMove = true;
        while(Input.GetAxisRaw("Horizontal") is not 0f || Input.GetAxisRaw("Vertical") is not 0f)
        {
            m_direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(Direction.x is not 0f)
            {
                m_direction.y = 0f;
            }

            Animator.SetFloat("DirX", Direction.x);
            Animator.SetFloat("DirY", Direction.y);

            var hit = GetComponent<ScanManager>().CheckCanMove();
            if(hit.transform is not null)
            {
                break;
            }

            Animator.SetBool("IsMove", true);

            while(CurrentWalkCount < WalkCount)
            {
                if(Direction.x is not 0f)
                {
                    transform.Translate(Direction.x * Speed, 0f, 0f);
                }
                else if(Direction.y is not 0f)
                {
                    transform.Translate(0f, Direction.y * Speed, 0f);
                }

                CurrentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            CurrentWalkCount = 0;
        }
        Debug.Log("여기");
        Animator.SetBool("IsMove", false);
        IsMove = false;
    }
}