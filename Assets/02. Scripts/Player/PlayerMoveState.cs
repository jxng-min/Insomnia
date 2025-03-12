using System.Collections;
using UnityEngine;

public class PlayerMoveState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    private bool m_is_moving = false;

    public void ExecuteEnter(PlayerCtrl sender)
    {
        if(m_player_ctrl is null)
        {
            m_player_ctrl = sender;
        }

    }

    public void Execute()
    {
        SetAnimation();

        if(GameManager.Instance.GameState == GameEventType.Playing)
        {
            // if(m_player_ctrl.Direction.magnitude > 0f && !m_is_moving)
            // {
            //     m_player_ctrl.NonPass = false;
            //     StartCoroutine(MoveCoroutine());
            // }
            m_player_ctrl.Rigidbody.linearVelocity = new Vector2(m_player_ctrl.Direction.x, m_player_ctrl.Direction.y) * 3f;

            if(m_player_ctrl.Direction.magnitude == 0f)
            {
                m_player_ctrl.ChangeState(PlayerState.Idle);
            }
        }
    }

    public void ExecuteExit()
    {

    }

    private void SetAnimation()
    {
        m_player_ctrl?.Animator.SetFloat("DirX", m_player_ctrl.Direction.x);
        m_player_ctrl?.Animator.SetFloat("DirY", m_player_ctrl.Direction.y);

        if(m_player_ctrl.NonPass)
        {
            m_player_ctrl?.Animator.SetBool("IsMove", true);
        }
    }

    private IEnumerator MoveCoroutine()
    {
        m_is_moving = true;
        while(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            yield return null;
            
            m_player_ctrl.Direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(m_player_ctrl.Direction.x != 0f)
                m_player_ctrl.Direction = new Vector3(m_player_ctrl.Direction.x, 0f, m_player_ctrl.Direction.z);
            
            RaycastHit2D hit = GetComponent<ScanManager>().CheckCanMove();
            if(hit.transform != null)
            {
                m_player_ctrl.NonPass = true;
                m_player_ctrl.ChangeState(PlayerState.Move);
                break;
            }

            m_player_ctrl.ChangeState(PlayerState.Move);

            while(m_player_ctrl.CurrentWalkCount < m_player_ctrl.WalkCount)
            {
                if(m_player_ctrl.Direction.x != 0)
                    transform.Translate(m_player_ctrl.Direction.x * m_player_ctrl.Speed, 0, 0);
                else if(m_player_ctrl.Direction.y != 0)
                    transform.Translate(0, m_player_ctrl.Direction.y * m_player_ctrl.Speed, 0);

                m_player_ctrl.CurrentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            m_player_ctrl.CurrentWalkCount = 0;
        }
        
        m_is_moving = false;
        m_player_ctrl.ChangeState(PlayerState.Idle);
    }
}