using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _State;
using _EventBus;

public class PlayerCtrl : MonoBehaviour
{
    public Rigidbody2D m_rigidbody;
    public Animator m_animator;

    public float m_player_speed = 2.0f;

    public Vector3 m_move_vec = new Vector3(0f, 0f, -90f);
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

        m_stop_state = gameObject.AddComponent<PlayerStopState>();
        m_move_state = gameObject.AddComponent<PlayerMoveState>();

        m_player_state_context = new PlayerStateContext(this);
        m_player_state_context.Transition(m_stop_state);
    }

    public void StopPlayer()
    {
        m_player_state_context.Transition(m_stop_state);
    }

    public void MovePlayer()
    {
        m_player_state_context.Transition(m_move_state);
    }

    public void SetAxis()
    {
        if(m_move_vec.x == 0f && m_move_vec.y == 0f)
            m_is_move = false;
        else
            m_is_move = true;
    }

    private void Update()
    {
        DataManager.Instance.m_now_player.m_player_position = transform.position;
        SetAxis();

        if(!GameManager.Instance.m_is_talk && GameManager.Instance.m_game_status != "pause")
            m_move_vec = new Vector3(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"), m_move_vec.z);

        if(m_is_move == false)
            StopPlayer();
        else
            MovePlayer();

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

    private void FixedUpdate()
    {
        if(GameManager.Instance.m_game_status == "playing")
            m_rigidbody.velocity = new Vector2(m_move_vec.x, m_move_vec.y) * m_player_speed;
        else
            m_rigidbody.velocity = Vector2.zero;
    }
}
