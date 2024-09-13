using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanManager : MonoBehaviour
{
    private Vector3 m_dir_vec;
    private GameObject m_scan_object;
    private PlayerCtrl m_player_ctrl;
    private GameObject m_end_cursor;

    private void Start()
    {
        m_player_ctrl = GetComponent<PlayerCtrl>();
        m_end_cursor = ObjectFindManager.FindInactiveObject("Cursor_Dialogue");
    }

    private void Update()
    {
        if(m_player_ctrl.m_move_vec.x != 0f)
        {
            if(m_player_ctrl.m_move_vec.x >= 0f)
                m_dir_vec = Vector3.right;
            else
                m_dir_vec = Vector3.left;
        }
        else if(m_player_ctrl.m_move_vec.y != 0f)
        {
            if(m_player_ctrl.m_move_vec.y >= 0f)
                m_dir_vec = Vector3.up;
            else
                m_dir_vec = Vector3.down;
        }

        if(GameManager.Instance.m_game_status == "playing" 
                                            && Input.GetButtonDown("Jump") 
                                            && m_scan_object != null
                                            && m_end_cursor.activeSelf
          )
        {
            GameManager.Instance.Talking(m_scan_object);
        }

    }

    private void FixedUpdate()
    {
        Debug.DrawRay(m_player_ctrl.m_rigidbody.position, m_dir_vec * 0.7f, new Color(0, 1, 0));
        
        RaycastHit2D ray_hit = Physics2D.Raycast(
                                                    m_player_ctrl.m_rigidbody.position, 
                                                    m_dir_vec, 
                                                    0.7f, 
                                                    LayerMask.GetMask("OBJECT")
                                                );

        if(ray_hit.collider)
            m_scan_object = ray_hit.collider.gameObject;
        else
            m_scan_object = null;
    }
}
