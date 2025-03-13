using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slots_root;

    private InventorySlot[] m_slots;
    public InventorySlot[] Slots
    {
        get { return m_slots; }
    }

    private void Awake()
    {
        m_slots = m_slots_root.GetComponentsInChildren<InventorySlot>();
    }

    private void Start()
    {
        LoadInventory();
    }

    public void AcquireItem(Item item, int count = 1)
    {
        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].Item is null)
            {
                m_slots[i].AddItem(item, count);
                return;
            }
        }
    }

    public int GetItemCount(ItemCode code)
    {
        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].Item is null)
            {
                continue;
            }

            if(m_slots[i].Item.ID == (int)code)
            {
                return m_slots[i].Item.ID;
            }
        }

        return 0;
    }

    public void LoadInventory()
    {
        for(int i = 0; i < DataManager.Instance.PlayerData.m_items.m_inventory.Length; i++)
        {
            Debug.Log(ItemDataManager.Instance.GetItemByID(DataManager.Instance.PlayerData.m_items.m_inventory[i].m_item_id).Name);
            m_slots[i].AddItem(ItemDataManager.Instance.GetItemByID(DataManager.Instance.PlayerData.m_items.m_inventory[i].m_item_id)
                                , DataManager.Instance.PlayerData.m_items.m_inventory[i].m_item_count);
        }
    }

    public InventoryDataList SaveInventory()
    {
        List<InventoryData> inventory_list = new List<InventoryData>();
        for(int i = 0; i < m_slots.Length; i++)
        {
            if(m_slots[i].Item is not null)
            {
                inventory_list.Add(new InventoryData(m_slots[i].Item.ID, m_slots[i].Count));
            }
        }

        InventoryDataList inventory_data = new InventoryDataList();
        inventory_data.m_inventory = inventory_list.ToArray();

        return inventory_data;
    } 
}
