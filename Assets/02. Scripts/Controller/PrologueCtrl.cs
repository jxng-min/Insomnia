using System.Collections;
using TMPro;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    [Header("오프닝 프롤로그를 출력할 라벨")]
    [SerializeField] private TMP_Text m_prologue_label;

    [Header("프롤로그 들어갈 텍스트 목록")]
    [SerializeField][TextArea] private string[] m_prologue_texts;

    private int m_current_index;

    private void Awake()
    {
        GameEventBus.Publish(GameEventType.Clear);
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM("Loading Background");

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        m_prologue_label.text = m_prologue_texts[m_current_index];

        yield return null;

        float elapsed_time = 0f;
        float target_time = 2f;

        Color color = m_prologue_label.color;

        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float t = elapsed_time / target_time;

            color.a = Mathf.Lerp(0f, 0.7f, t);
            m_prologue_label.color = color;
        }

        color.a = 0.7f;
        m_prologue_label.color = color;

        elapsed_time = 0f;

        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float t = elapsed_time / target_time;

            color.a = Mathf.Lerp(0.7f, 0f, t);
            m_prologue_label.color = color;
        }

        if(m_current_index < 2)
        {
            m_current_index++;

            yield return StartCoroutine(Fade());
        }
        else
        {
            yield return new WaitForSeconds(1f);
            LoadingManager.Instance.LoadScene("Game");
        }
    }
}
