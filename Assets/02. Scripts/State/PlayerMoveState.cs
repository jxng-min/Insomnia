using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _State
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerCtrl m_player_ctrl;

        public void Handle(PlayerCtrl player_ctrl)
        {
            if(!m_player_ctrl)
                m_player_ctrl = player_ctrl;

            m_player_ctrl.m_animator.SetFloat("DirX", m_player_ctrl.m_move_vec.x);
            m_player_ctrl.m_animator.SetFloat("DirY", m_player_ctrl.m_move_vec.y);
            m_player_ctrl.m_animator.SetBool("IsMove", true);
        }
    }
}