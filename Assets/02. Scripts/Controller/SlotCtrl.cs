using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class SlotCtrl : MonoBehaviour
{
    public string m_item_name;
    
    [SerializeField]
    private TMP_Text m_item_text;

    public void AddItem(string item_name)
    {
        m_item_name = item_name;
        m_item_text.text = m_item_name;
    }

    private void ClearSlot()
    {
        m_item_name = "";
        m_item_text.text = m_item_name;
    }
}
