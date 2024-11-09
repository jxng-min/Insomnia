using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject m_player;
    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(m_player)
            transform.position = new Vector3(m_player.transform.position.x, 
                                                m_player.transform.position.y, -10);
    }
}
