using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Singleton;
using System.Net.NetworkInformation;

public class NarraitionManager : MonoBehaviour
{
    public CanvasGroup m_panel_canvas_group;
    public float m_fade_duration = 1.0f;
    private Dictionary<int, string[]> m_narration_data;
    private Dictionary<int, int> m_narration_list;
    private int m_narration_idx;
    public bool m_is_narration = false;

    private TypeEffectCtrl m_type_effect;

    private void Start()
    {
        m_narration_list = new Dictionary<int, int>();
        GenerateNarration();

        m_narration_data = new Dictionary<int, string[]>();
        GenerateData();

        m_type_effect = ObjectFindManager.FindInactiveObject("Text_Narraition").GetComponent<TypeEffectCtrl>();
    }

    public string GetNarraition(int id, int narraition_idx)
    {
        if(narraition_idx == m_narration_data[id].Length)
            return null;
        else
            return m_narration_data[id][narraition_idx];
    }

    private void GenerateData()
    {
        m_narration_data.Add(10, new string[] {"며칠 째, 잠에 들기 위해 눈을 감으면", "누군가 나에게 속삭이는 소리가 들린다.", "오늘 이 소리가 들리는 이유를 찾아보려고 한다.", "..."});
        m_narration_data.Add(20, new string[] {"\"하아.. OO아. 그래서 날 집으로 부른 이유가 뭐야?\"", "\"다시 만나면 안되냐고?\n너 이미 내가 준 편지 읽어본거 아니야?\"", "\"야... 너 손에 그거 뭐야..\"", "\"OO아.. 우리 말로 하자.. 응..??\""});
        m_narration_data.Add(30, new string[] {"\"OO아! 우리 꼭 나중에 결혼하자!\"", "\"너가 지금 힘들어하지만,\n내가 옆에 꼭 있을거니깐!\"", "\"그니깐 우리 잘 이겨내고\n꼭 평생 함께 하는거야!! 알겠지?\""});
        m_narration_data.Add(40, new string[] {"집 밖으로 나오니 빨간 빛과 파란 빛이 눈에 비치고 있었다.", "경찰들이 날 보니 내게로 다가왔다.\n수갑을 내 손에 채우는게 느껴졌다.", "...", "엔딩: [체포]"});
        m_narration_data.Add(50, new string[] {});
    }

    private void GenerateNarration()
    {
        m_narration_list.Add(10, 4);
        m_narration_list.Add(20, 4);
        m_narration_list.Add(30, 3);
        m_narration_list.Add(40, 4);
        m_narration_list.Add(50, 0);
    }

    private IEnumerator FadeOut()
    {
        float elapsed_time = 0f;

        m_panel_canvas_group.interactable = true;
        m_panel_canvas_group.blocksRaycasts = true;

        while (elapsed_time < m_fade_duration)
        {
            elapsed_time += Time.deltaTime;
            m_panel_canvas_group.alpha = Mathf.Lerp(0f, 1f, elapsed_time / m_fade_duration);
            yield return null;
        }
        m_panel_canvas_group.alpha = 1f;
    }

    private IEnumerator FadeIn()
    {
        float elapsed_time = 0f;

        while (elapsed_time < m_fade_duration)
        {
            elapsed_time += Time.deltaTime;
            m_panel_canvas_group.alpha = Mathf.Lerp(1f, 0f, elapsed_time / m_fade_duration);
            yield return null;
        }

        m_panel_canvas_group.alpha = 0f;
        m_panel_canvas_group.interactable = false;
        m_panel_canvas_group.blocksRaycasts = false;
    }

    public void CheckNarraition(int id)
    {
        if(m_narration_idx < m_narration_list[DataManager.Instance.m_now_player.m_nar_id])
            m_narration_idx++;
        else
        {
            DataManager.Instance.m_now_player.m_nar_actions[id] = true;
            DataManager.Instance.m_now_player.m_nar_id += 10;
        }
    }

    private void StartNarraition(int id)
    {
        StartCoroutine(FadeOut());
        Narraition(id);
    }

    private void Narraition(int id)
    {
        SoundManager.Instance.m_bgm.Pause();
        GameManager.Instance.m_game_status = "pause";
        m_panel_canvas_group.alpha = 1f;

        int narration_talk_idx = DataManager.Instance.m_now_player.m_nar_id;
        string narration_data = GetNarraition(narration_talk_idx, m_narration_idx);     

        if(narration_data == null)
        {
            CheckNarraition(id);
            m_narration_idx = 0;

            if(DataManager.Instance.m_now_player.m_nar_id != 50)
            {
                SoundManager.Instance.ButtonClick();
                StartCoroutine(FadeIn());

                m_is_narration = false;
                SoundManager.Instance.m_bgm.UnPause();
                GameManager.Instance.m_game_status = "playing";
                return;
            }
            else
                if(Input.GetButtonDown("Jump"))
                    GameManager.Instance.Finish();
        }

        m_type_effect.SetText(narration_data);
        m_is_narration = true;
        m_narration_idx++;   
    }

    private void Update()
    {
        if(GameManager.Instance.m_game_status == "playing" || m_is_narration == true)
        {
            switch(DataManager.Instance.m_now_player.m_quest_id)
            {
            case 10:
                if(DataManager.Instance.m_now_player.m_nar_actions[0] == false && m_narration_idx == 0)
                    Narraition(0);
                else if(DataManager.Instance.m_now_player.m_nar_actions[0] == false && Input.GetButtonDown("Jump") && m_type_effect.GetCursorState())
                    Narraition(0);
                break;

            case 80:
                if(DataManager.Instance.m_now_player.m_nar_actions[1] == false && m_narration_idx == 0)
                    StartNarraition(1);
                else if(!DataManager.Instance.m_now_player.m_nar_actions[1] && Input.GetButtonDown("Jump") && m_type_effect.GetCursorState())
                    Narraition(1);
                break;
            
            case 100:
                if(DataManager.Instance.m_now_player.m_nar_actions[2] == false && m_narration_idx == 0)
                    StartNarraition(2);
                else if(!DataManager.Instance.m_now_player.m_nar_actions[2] && Input.GetButtonDown("Jump") && m_type_effect.GetCursorState())
                    Narraition(2);
                break;
            
            case 140:
                if(DataManager.Instance.m_now_player.m_nar_actions[3] == false && m_narration_idx == 0)
                    StartNarraition(3);
                else if(!DataManager.Instance.m_now_player.m_nar_actions[3] && Input.GetButtonDown("Jump") && m_type_effect.GetCursorState())
                    Narraition(3);
                break;
            
            default:
                break;
            }
        }
    }
}
