using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _State
{
    public class PlayerStopState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if(!m_player_ctrl)
                m_player_ctrl = player_ctrl;

            Vector2 from_point = m_player_ctrl.transform.position;
            Vector2 to_point = new Vector2(from_point.x + m_player_ctrl.m_move_vec.x,
                                            from_point.y + m_player_ctrl.m_move_vec.y);
            m_player_ctrl.m_move_vec.z = GetAngle(from_point, to_point);

            SetStopAnimation();
        }

        private float GetAngle(Vector2 p1, Vector2 p2)
        {
            float angle = 0f;

            if(m_player_ctrl.m_move_vec.x != 0f || m_player_ctrl.m_move_vec.y != 0f)
            {
                float dx = p2.x - p1.x;
                float dy = p2.y - p1.y;

                float rad = Mathf.Atan2(dy, dx);
                angle = rad * Mathf.Rad2Deg;
            }
            else
                angle = m_player_ctrl.m_move_vec.z;
            
            return angle;
        }

        private void SetStopAnimation()
        {
            m_player_ctrl.m_animator.SetBool("IsMove", false);
            m_player_ctrl.m_animator.ResetTrigger("Up");
            m_player_ctrl.m_animator.ResetTrigger("Down");
            m_player_ctrl.m_animator.ResetTrigger("Left");
            m_player_ctrl.m_animator.ResetTrigger("Right");
        }
    }
}

