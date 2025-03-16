using System.Collections;
using TMPro;
using UnityEngine;

public class EpilogueCtrl : MonoBehaviour
{
    [Header("엔딩 에필로그를 출력할 라벨")]
    [SerializeField] private TMP_Text m_credit_label;

    [Header("에필로그에 들어갈 텍스트 목록")]
    [SerializeField][TextArea] private string[] m_credit_texts;

    private int m_current_index;
    private int m_current_count;

    private void Awake()
    {
        GameEventBus.Publish(GameEventType.Clear);
    }

    private void Start()
    {
        // TODO: 에필로그 백그라운드

        if(GameManager.Instance.EndingType == 1)
        {
            StartCoroutine(Fade(0, 4));
        }
        else if(GameManager.Instance.EndingType == 2)
        {
            StartCoroutine(Fade(4, 5));
        }
    }

    private IEnumerator Fade(int start_index, int count)
    {
        m_current_index = start_index;
        m_credit_label.text = m_credit_texts[m_current_index];

        yield return null;

        float elapsed_time = 0f;
        float target_time = 2f;

        Color color = m_credit_label.color;

        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float t = elapsed_time / target_time;

            color.a = Mathf.Lerp(0f, 0.7f, t);
            m_credit_label.color = color;
        }

        color.a = 0.7f;
        m_credit_label.color = color;

        elapsed_time = 0f;

        while(elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;
            yield return null;

            float t = elapsed_time / target_time;

            color.a = Mathf.Lerp(0.7f, 0f, t);
            m_credit_label.color = color;
        }

        if(m_current_count < count - 1)
        {
            m_current_index++;
            m_current_count++;

            yield return StartCoroutine(Fade(m_current_index, count));
        }
        else
        {
            yield return new WaitForSeconds(1f);
            LoadingManager.Instance.LoadScene("Title");
        }
    }
}
