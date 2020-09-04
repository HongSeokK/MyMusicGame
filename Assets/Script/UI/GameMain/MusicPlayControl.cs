using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace MusicGame.UI.GameMain
{
    /// <summary>
    /// ゲームで使われる楽曲制御のスクリプト
    /// </summary>
    /// @j_choi
    public class MusicPlayControl : MonoBehaviour
    {
        /// <summary>
        /// AudioSource Component
        /// </summary>
        [SerializeField] private AudioSource m_AudioSource = null;

        /// <summary>
        /// 音源
        /// </summary>
        [SerializeField] private AudioClip m_AudioClip = null;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Init()
        {
            if (m_AudioSource == null || m_AudioClip == null)
            {
                Debug.LogError("Component NULL!");
            }
            m_AudioSource.clip = m_AudioClip;
            m_AudioSource.playOnAwake = false;
            return true;
        }

        /// <summary>
        /// 音源を再生
        /// </summary>
        /// <param name="isLoop">反復再生</param>
        /// <returns>音源再生に成功したら、Trueを返す</returns>
        public async Task<bool> Play(bool isLoop)
        {
            if (m_AudioSource == null) return false;
            if (m_AudioClip == null) return false;
            m_AudioSource.loop = isLoop;
            m_AudioSource.Play();
            return true;
        }


        /// <summary>
        /// 動画再生を注視する
        /// </summary>
        /// <returns>動画再生注視にしたら、Trueを返す</returns>
        public async Task<bool> Stop()
        {
            if (m_AudioSource == null) return false;
            if (m_AudioClip == null) return false;
            m_AudioSource.Stop();
            return true;
        }
    }
}