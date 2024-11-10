using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrickManager : MonoBehaviour
{
    public List<GameObject> m_images = new List<GameObject>();
    public bool[] m_images_active = new bool[3];
    public List<GameObject> m_objects = new List<GameObject>();
    
    private void Start()
    {
        for(int i = 0; i < 3; i++)
            m_images_active[i] = DataManager.Instance.m_now_player.m_trick_images[i];
    }

    private IEnumerator ShowImage(int idx)
    {
        m_images[idx].SetActive(true);
        m_images_active[idx] = true;
        yield return new WaitForSeconds(1f);
        m_images[idx].SetActive(false);
    }

    void Update()
    {
        if(GameManager.Instance.m_game_status == "playing")
        {
            switch(DataManager.Instance.m_now_player.m_quest_id)
            {
            case 40:
                if(!m_images_active[0])
                {
                    SoundManager.Instance.BedTrick();
                    StartCoroutine(ShowImage(0));
                }
                break;

            case 120:
                if(!m_images_active[1])
                {
                    SoundManager.Instance.KnifeTrick();
                    StartCoroutine(ShowImage(1));
                }
                break;

            case 140:
                if(!m_images_active[2])
                {
                    SoundManager.Instance.PoliceTrick();
                    StartCoroutine(ShowImage(2));
                }
                break;
            }
        }
    }
}
