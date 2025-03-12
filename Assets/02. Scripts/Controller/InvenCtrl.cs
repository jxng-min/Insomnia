using UnityEngine;

public class InvenCtrl : MonoBehaviour
{
    private SlotCtrl[] m_slots;

    private void OnEnable()
    {
        m_slots = GetComponentsInChildren<SlotCtrl>();

        for(int i = 0; i < DataManager.Instance.PlayerData.m_items.Count; i++)
            AcquireItem(DataManager.Instance.PlayerData.m_items[i]);
    }

    private void AcquireItem(string item_name)
    {
        for (int i = 0; i < m_slots.Length; i++)
            if(m_slots[i].m_item_name == item_name)
                return;

        for(int i = 0; i < m_slots.Length; i++)
            if(m_slots[i].m_item_name == "")
            {
                m_slots[i].AddItem(item_name);
                return;
            }
    }
}
