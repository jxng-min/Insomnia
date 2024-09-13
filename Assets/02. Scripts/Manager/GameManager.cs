using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _Singleton;
using _EventBus;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string m_game_status;

    private Animator m_dialogue_panel;
    private TypeEffectCtrl m_type_effect;
    private GameObject m_pause_panel;

    public GameObject m_scan_object;
    public bool m_is_talk = false;
    public int m_talk_idx;

    private void Start()
    {
        GameEventBus.Subscribe(GameEventType.WAIT, Wait);
        GameEventBus.Publish(GameEventType.WAIT);

        SoundManager.Instance.TitleBackground();
    }

    private void Update()
    {
        if(DataManager.Instance.m_now_player.m_nar_actions[3])
        {
            Finish();
            if(Input.GetButtonDown("Jump"))
                SceneManager.LoadScene("Title");
        }
    }

    public void Wait()
    {
        m_game_status = "wait";
    }

    public void Playing()
    {
        m_dialogue_panel = ObjectFindManager.FindInactiveObject("Dialogue_Bar").GetComponent<Animator>();
        m_type_effect = ObjectFindManager.FindInactiveObject("Text_Dialogue").GetComponent<TypeEffectCtrl>();
        
        m_pause_panel = ObjectFindManager.FindInactiveObject("Pause_Panel");
        m_pause_panel.SetActive(false);

        m_game_status = "playing";
    }

    public void Pause()
    {
        m_pause_panel.SetActive(true);
        m_game_status = "pause";
    }

    public void Finish()
    {
        m_game_status = "finish";
        SceneManager.LoadScene("Title");
    }

    public void Dead()
    {
        m_game_status = "dead";
    }

    public void Talking(GameObject scan_object)
    {
        m_is_talk = true;
        m_scan_object = scan_object;

        ObjectData object_data = m_scan_object.GetComponent<ObjectData>();
        Talk(object_data.m_id, object_data.m_is_npc);

        m_dialogue_panel.SetBool("Is_Show", m_is_talk);
    }

    private void Talk(int id, bool is_npc)
    {
        int quest_talk_idx = QuestManager.Instance.GetQuestDialogueIndex(id);
        string dialogue_data = DialogueManager.Instance.GetDialogue(id + quest_talk_idx, m_talk_idx);

        if(dialogue_data == null)
        {
            m_is_talk = false;
            m_talk_idx = 0;

            SoundManager.Instance.ButtonClick();
            QuestManager.Instance.CheckQuest(id);
            return;
        }

        m_type_effect.SetText(dialogue_data);
        m_is_talk = true;
        m_talk_idx++;
    }
}
