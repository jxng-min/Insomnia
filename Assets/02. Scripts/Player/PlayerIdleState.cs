using UnityEngine;

    public class PlayerIdleState : MonoBehaviour, IState<PlayerCtrl>
    {
        private PlayerCtrl m_player_ctrl;

        public void ExecuteEnter(PlayerCtrl sender)
        {
            if(m_player_ctrl is null)
            {
                m_player_ctrl = sender;
            }

            m_player_ctrl.Animator.SetBool("IsMove", false);
        }

        public void Execute()
        {
            if(m_player_ctrl.Direction.magnitude > 0f)
            {
                m_player_ctrl.ChangeState(PlayerState.Move);
            }
            else
            {
                m_player_ctrl.ChangeState(PlayerState.Idle);
            }
        }

        public void ExecuteExit()
        {

        }
    }