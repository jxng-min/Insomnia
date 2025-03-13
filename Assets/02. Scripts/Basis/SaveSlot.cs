using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("슬롯의 버튼")]
    [SerializeField] private Button m_button;
    public Button Button
    {
        get { return m_button; }
    }

    [Header("슬롯의 이름")]
    [SerializeField] private TMP_Text m_title_label;
    public TMP_Text Title
    {
        get { return m_title_label; }
    }

    [Header("슬롯의 플레이 시간")]
    [SerializeField] private TMP_Text m_time_label;
    public TMP_Text Time
    {
        get { return m_time_label; }
    }
}
