using TMPro;
using UnityEngine;

public class TimerCtrl : MonoBehaviour
{
    [Header("시간 UI TMP")]
    [SerializeField] private TMP_Text m_time_label;
    private void Update()
    {
        if(GameManager.Instance.GameState == GameEventType.Playing)
        {
            DataManager.Instance.PlayerData.m_play_time += Time.deltaTime;

            int min = Mathf.FloorToInt(DataManager.Instance.PlayerData.m_play_time / 60);
            int sec = Mathf.FloorToInt(DataManager.Instance.PlayerData.m_play_time % 60);

            m_time_label.text = $"플레이 타임 [{min:D2} : {sec:D2}]";
        }
    }
}
