using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int m_item_id;
    public int m_item_count;

    public InventoryData(int item_id, int item_count)
    {
        m_item_id = item_id;
        m_item_count = item_count;
    }
}

[System.Serializable]
public class InventoryDataList
{
    public InventoryData[] m_inventory;

    public InventoryDataList()
    {
        m_inventory = new InventoryData[0];
    }
}