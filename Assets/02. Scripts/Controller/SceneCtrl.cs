using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public string m_scene_name;

    public void SceneChange()
    {
        SceneManager.LoadScene(m_scene_name);
    }
}
