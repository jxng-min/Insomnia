using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Scriptable Object/Create Object")]
public class ObjectData : ScriptableObject
{
    [Header("오브젝트의 고유한 ID")]
    [SerializeField] private int m_id;
    public int ID
    {
        get { return m_id; }
    }

    [Space(30)]
    [Header("오브젝트의 기본 대사")]
    [SerializeField][TextArea] private string[] m_default_diagloue;
    public string[] Dialogue
    {
        get { return m_default_diagloue; }
    }
}