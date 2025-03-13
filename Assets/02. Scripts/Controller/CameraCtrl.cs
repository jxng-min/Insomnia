using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(GameManager.Instance.Player.transform.position.x, 
                                            GameManager.Instance.Player.transform.position.y, -10);
    }
}
