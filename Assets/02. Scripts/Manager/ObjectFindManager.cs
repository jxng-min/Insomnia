using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectFindManager : MonoBehaviour
{
    public static GameObject FindInactiveObject(string name)
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] root_objects = scene.GetRootGameObjects();

        foreach (GameObject obj in root_objects)
        {
            Transform[] transforms = obj.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in transforms)
            {
                if (t.gameObject.name == name)
                    return t.gameObject;
            }
        }

        return null;
    }  
}
