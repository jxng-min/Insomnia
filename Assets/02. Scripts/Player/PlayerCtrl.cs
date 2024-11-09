using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _State;
using _EventBus;
using UnityEditor.Rendering;

public class PlayerCtrl : MonoBehaviour
{
    public Rigidbody2D m_rigidbody;
    public Animator m_animator;
    public BoxCollider2D m_box_collider;

    public LayerMask m_layer_mask;
    public bool m_is_no_passing = false;

    public float m_player_speed = 0.02f;
    public Vector3 m_move_vec;
    public int m_walk_count;
    private int m_current_walk_count;
    public bool m_is_move = false;

    private IPlayerState m_stop_state, m_move_state;
    private PlayerStateContext m_player_state_context;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Subscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Subscribe(GameEventType.FINISH, GameManager.Instance.Finish);
        GameEventBus.Subscribe(GameEventType.DEAD, GameManager.Instance.Dead);

        SoundManager.Instance.GameBackground();

        transform.position = DataManager.Instance.m_now_player.m_player_position;
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Unsubscribe(GameEventType.PAUSE, GameManager.Instance.Pause);
        GameEventBus.Unsubscribe(GameEventType.FINISH, GameManager.Instance.Finish);
        GameEventBus.Unsubscribe(GameEventType.DEAD, GameManager.Instance.Dead);

        SoundManager.Instance.TitleBackground();

        DataManager.Instance.m_now_slot = -1;
    }

    private void Start()
    {
        GameEventBus.Publish(GameEventType.PLAYING);

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_box_collider = GetComponent<BoxCollider2D>();

        m_stop_state = gameObject.AddComponent<PlayerStopState>();
        m_move_state = gameObject.AddComponent<PlayerMoveState>();

        m_player_state_context = new PlayerStateContext(this);
        m_player_state_context.Transition(m_stop_state);
    }

    private void Update()
    {
        DataManager.Instance.m_now_player.m_player_position = transform.position;

        if(!GameManager.Instance.m_is_talk && GameManager.Instance.m_game_status != "pause")
        {
            if(!m_is_move)
            {
                if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    m_is_no_passing = false;
                    m_is_move = true;
                    StartCoroutine(MoveCoroutine());
                }
            }

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.ButtonClick();

            if(GameManager.Instance.m_game_status != "pause")
            {
                SoundManager.Instance.m_bgm.Pause();
                GameEventBus.Publish(GameEventType.PAUSE);
            }
            else if(GameObject.Find("Title_Inner_Panel") == null && GameObject.Find("Item_Inner_Panel") == null && GameObject.Find("Save_Inner_Panel") == null)
            {
                SoundManager.Instance.m_bgm.UnPause();
                GameEventBus.Publish(GameEventType.PLAYING);
            }
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            m_move_vec.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(m_move_vec.x != 0)
                m_move_vec.y = 0;
            
            RaycastHit2D hit = GetComponent<ScanManager>().CheckCanMove();
            if(hit.transform != null)
            {
                m_is_no_passing = true;
                MovePlayer();
                break;
            }

            MovePlayer();

            while(m_current_walk_count < m_walk_count)
            {
                if(m_move_vec.x != 0)
                    transform.Translate(m_move_vec.x * m_player_speed, 0, 0);
                else if(m_move_vec.y != 0)
                    transform.Translate(0, m_move_vec.y * m_player_speed, 0);

                m_current_walk_count++;
                yield return new WaitForSeconds(0.01f);
            }
            m_current_walk_count = 0;
        }
        m_is_move = false;
        
        StopPlayer();
    }

    public void StopPlayer()
    {
        m_player_state_context.Transition(m_stop_state);
    }

    public void MovePlayer()
    {
        m_player_state_context.Transition(m_move_state);
    }
}