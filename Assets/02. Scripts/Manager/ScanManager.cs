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
                int quest_id = -1;
                switch(m_current_object.Data.ID)
                {
                    case (int)ObjectCode.Computer:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 0:
                                    CheckQuest(quest_id, 0, 3, 0, 0, 3, 2);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;
                    }

                    case (int)ObjectCode.Bed2:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 1:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 3);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;
                    }

                    case (int)ObjectCode.Bed1:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            Debug.Log(quest_id);
                            switch(quest_id)
                            {
                                case 2:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 4);
                                    break;
                                
                                case 11:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 4, 6);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;
                    }

                    case (int)ObjectCode.BookShelf0:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 3:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 4);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;                        
                    }

                    case (int)ObjectCode.LibraryDesk:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 4:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 6);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;                        
                    }

                    case (int)ObjectCode.BookShelf2:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 5:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 8);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }

                    case (int)ObjectCode.Box1:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 6:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 5);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }

                    case (int)ObjectCode.TV:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 7:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 4);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }

                    case (int)ObjectCode.Carrier:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 8:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 3);
                                    break;
                                
                                case 10:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 3, 4);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }

                    case (int)ObjectCode.Guitar0:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 9:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 4);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }                    

                    case (int)ObjectCode.Exit:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 13:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 0);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }           

                    case (int)ObjectCode.Terrace:
                    {
                        if(m_current_object.GetComponent<QuestObject>().IsExistQuest(out quest_id))
                        {
                            switch(quest_id)
                            {
                                case 12:
                                    CheckQuest(quest_id, 0, 0, 0, 0, 0, 0);
                                    break;
                            }
                        }
                        else
                        {
                            DialogueManager.Instance.Dialoging(m_current_object);
                        }
                        break;   
                    }           

                    default:
                        DialogueManager.Instance.Dialoging(m_current_object);
                        break;
                }
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

    private void CheckQuest(int quest_id, int never_index, int never_count, int going_index, int going_count, int clear_index, int clear_count)
    {
        if(quest_id == -1)
        {
            return;
        }

        if(QuestManager.Instance.CheckQuestState(quest_id) == QuestState.NEVER_RECEIVED)
        {
            DialogueManager.Instance.Dialoging(m_current_object, never_index, never_count);

            if(!DialogueManager.Instance.IsTalking)
            {
                QuestManager.Instance.ReceiveQuest(quest_id);
            }
        }
        else if(QuestManager.Instance.CheckQuestState(quest_id) == QuestState.ON_GOING)
        {
            DialogueManager.Instance.Dialoging(m_current_object, going_index, going_count);
        }
        else if(QuestManager.Instance.CheckQuestState(quest_id) == QuestState.CLEAR)
        {
            DialogueManager.Instance.Dialoging(m_current_object, clear_index, clear_count);

            if(!DialogueManager.Instance.IsTalking)
            {
                QuestManager.Instance.CompleteQuest(quest_id);
            }            
        }
    }
}
