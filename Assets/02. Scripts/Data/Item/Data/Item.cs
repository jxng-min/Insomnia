using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Create Item")]
public class Item : ScriptableObject
{
    [Header("아이템의 고유한 ID")]
    [SerializeField] private int m_item_id;
    public int ID
    {
        get { return m_item_id; }
    }

    [Header("아이템의 고유한 이름")]
    [SerializeField] private string m_item_name;
    public string Name
    {
        get { return m_item_name; }
    }
}
