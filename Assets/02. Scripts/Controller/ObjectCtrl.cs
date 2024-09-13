using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCtrl : MonoBehaviour
{
    public GameObject m_obj;

    public void EnableObject()
    {
        m_obj.SetActive(true);
    }

    public void DisableObject()
    {
        m_obj.SetActive(false);
    }
}
