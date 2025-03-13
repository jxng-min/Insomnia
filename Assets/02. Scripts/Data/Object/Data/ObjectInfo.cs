using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [Header("이 오브젝트가 가지는 데이터")]
    [SerializeField] private ObjectData m_object_data;
    public ObjectData Data
    {
        get { return m_object_data; }
    }
}