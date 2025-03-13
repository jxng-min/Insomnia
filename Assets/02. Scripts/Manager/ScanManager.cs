using UnityEngine;

public class ScanManager : MonoBehaviour
{
    private Vector3 m_ray_direction;
    public Vector3 Direction
    {
        get { return m_ray_direction; }
        private set { m_ray_direction = value; }
    }

    private ObjectInfo m_current_object;
    private bool m_can_interaction = false;

    private void Update()
    {
        SetRayDirection();
        CheckObject();

        if(m_can_interaction)
        {
            if(GameManager.Instance.GameState == GameEventType.Playing
                                                        && Input.GetButtonDown("Jump") 
                                                        && m_current_object is not null
                                                        && DialogueManager.Instance.Cursor.activeSelf)
            {
                DialogueManager.Instance.Dialoging(m_current_object);
            }
        }
    }

    private void CheckObject()
    {
        Debug.DrawRay(GameManager.Instance.Player.Rigidbody.position, Direction * 1.25f, new Color(0, 1, 0));
        
        RaycastHit2D ray_hit = Physics2D.Raycast(
                                                    GameManager.Instance.Player.Rigidbody.position, 
                                                    Direction, 
                                                    1.25f, 
                                                    LayerMask.GetMask("OBJECT")
                                                );

        if(ray_hit.collider)
        {
            m_current_object = ray_hit.transform.GetComponent<ObjectInfo>();
            m_can_interaction = true;
        }
        else
        {
            m_current_object = null;
            m_can_interaction = false;
        }
    }

    private void SetRayDirection()
    {
        if(GameManager.Instance.Player.Direction.x is not 0f)
        {
            if(GameManager.Instance.Player.Direction.x >= 0f)
            {
                Direction = Vector3.right;
            }
            else
            {
                Direction = Vector3.left;
            }
        }
        else if(GameManager.Instance.Player.Direction.y is not 0f)
        {
            if(GameManager.Instance.Player.Direction.y >= 0f)
            {
                Direction = Vector3.up;
            }
            else
            {
                Direction = Vector3.down;
            }
        }
    }

    public RaycastHit2D CheckCanMove()
    {
        Vector2 move_start = GameManager.Instance.Player.transform.position;
        Vector2 move_end = move_start + new Vector2 (
                                                        GameManager.Instance.Player.Direction.x * GameManager.Instance.Player.Speed * GameManager.Instance.Player.WalkCount,
                                                        GameManager.Instance.Player.Direction.y * GameManager.Instance.Player.Speed * GameManager.Instance.Player.WalkCount
                                                    );

        GameManager.Instance.Player.Collider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(move_start, move_end, GameManager.Instance.Player.m_layer_mask);
        GameManager.Instance.Player.Collider.enabled = true;

        return hit;
    }
}
