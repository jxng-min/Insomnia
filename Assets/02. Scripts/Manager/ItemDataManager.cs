using UnityEngine;

public class ItemDataManager : Singleton<ItemDataManager>
{
    [Header("아이템 Scriptable Object 목록")]
    [SerializeField] private Item[] m_items;

    public Item GetItemByID(int item_id)
    {
        for(int i = 0; i < m_items.Length; i++)
        {
            if(m_items[i].ID == item_id)
            {
                return m_items[i];
            }
        }

        return null;
    }
}
