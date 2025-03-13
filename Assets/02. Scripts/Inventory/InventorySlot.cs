using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Item m_item;
    public Item Item
    {
        get { return m_item; }
        set { m_item = value; }
    }

    private int m_item_count;
    public int Count
    {
        get { return m_item_count; }
        set { m_item_count = value; }
    }

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private TMP_Text m_item_name_label;

    public void AddItem(Item item, int count = 1)
    {
        Item = item;
        Count = count;

        m_item_name_label.text = item.Name;
    }

    public void ClearSlot()
    {
        Item = null;
        Count = 0;

        m_item_name_label.text = null;
    }
}
