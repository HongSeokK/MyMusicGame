using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Type = MusicGame.Common.SceneCommon.SoundEffect;

namespace MusicGame.Scene.GameMain
{

    public class SoundEffectControl : MonoBehaviour
    {
        [SerializeField] private AudioClip m_miss;
        [SerializeField] private AudioClip m_normal;
        [SerializeField] private AudioClip m_perfect;
        [SerializeField] private AudioClip m_finish;
        [SerializeField] private AudioSource m_audioSource;

        public void Play(Type type)
        {
            switch(type)
            {
                case Type.Finish:
                    m_audioSource.clip = m_finish;
                    break;
                case Type.Miss:
                    m_audioSource.clip = m_miss;
                    break;
                case Type.Normal:
                    m_audioSource.clip = m_normal;
                    break;
                case Type.Perfect:
                    m_audioSource.clip = m_perfect;
                    break;
            }
            if(type == Type.Finish)
            {
                if(m_audioSource.isPlaying) m_audioSource.Stop();
                m_audioSource.Play();
                return;
            }
            m_audioSource.Play();
        }
    }
}