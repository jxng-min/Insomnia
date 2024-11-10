using System.Collections;
using System.Collections.Generic;
using _Singleton;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_bgm;
    public AudioSource m_audio_source;

    public AudioClip m_button_select_effect;
    public AudioClip m_button_push_effect;
    public AudioClip m_title_background;
    public AudioClip m_game_background;

    public AudioClip m_bed_trick;
    public AudioClip m_knife_trick;
    public AudioClip m_police_trick;
    public AudioClip m_escape_trick;

    private void Start()
    {
        SoundManager.Instance.Initialize();
    }

    public void Initialize()
    {
        m_button_select_effect = Resources.Load<AudioClip>("06. Sounds/Effect/Button_Click");
        m_button_push_effect = Resources.Load<AudioClip>("06. Sounds/Effect/Save_Success");
        m_title_background = Resources.Load<AudioClip>("06. Sounds/Background/Title_Background");
        m_game_background = Resources.Load<AudioClip>("06. Sounds/Background/InGame_Background");
        m_bed_trick = Resources.Load<AudioClip>("06. Sounds/InGame/Bed");
        m_knife_trick = Resources.Load<AudioClip>("06. Sounds/InGame/Knife");
        m_police_trick = Resources.Load<AudioClip>("06. Sounds/InGame/Siren");
        m_escape_trick = Resources.Load<AudioClip>("06. Sounds/InGame/Escape");

        if(m_audio_source == null && m_bgm == null)
        {
            m_audio_source = gameObject.AddComponent<AudioSource>();
            m_bgm = gameObject.AddComponent<AudioSource>();
        }

        m_bgm.loop = true;
        m_bgm.volume = 0.3f;

        m_audio_source.volume = 0.3f;
        m_audio_source.playOnAwake = false;
    }

    public void ButtonSelect()
    {
        m_audio_source.PlayOneShot(m_button_select_effect);
    }

    public void ButtonClick()
    {
        m_audio_source.PlayOneShot(m_button_push_effect);
    }

    public void TitleBackground()
    {
        if(m_title_background != null)
        {
            m_bgm.clip = m_title_background;
            m_bgm.Play();
        }
    }

    public void GameBackground()
    {
        if(m_game_background != null)
        {
            m_bgm.clip = m_game_background;
            m_bgm.Play();
        }
    }

    public void BedTrick()
    {
        m_audio_source.PlayOneShot(m_bed_trick);
    }

    public void KnifeTrick()
    {
        m_audio_source.PlayOneShot(m_knife_trick);
    }

    public void PoliceTrick()
    {
        m_audio_source.PlayOneShot(m_police_trick);
    }

    public void EscapeTrick()
    {
        m_audio_source.PlayOneShot(m_escape_trick);
    }
}
