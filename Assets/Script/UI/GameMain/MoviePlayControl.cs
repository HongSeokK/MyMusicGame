using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Threading.Tasks;

namespace MusicGame.UI.GameMain
{
    /// <summary>
    /// 動画再生管理スクリプト
    /// </summary>
    /// @j_choi
    public class MoviePlayControl : MonoBehaviour
    {
        /// <summary>
        /// 動画を表示させるスクリーン(RawImage)
        /// </summary>
        [SerializeField] private RawImage m_Screen = null;

        /// <summary>
        /// Video Player Component
        /// </summary>
        [SerializeField] private VideoPlayer m_VideoPlayer = null;

        /// <summary>
        /// 動画準備
        /// </summary>
        /// <returns>準備が終わったらTrue</returns>
        private async Task<bool> PrepareVideo()
        {
            m_VideoPlayer.Prepare();
            while (!m_VideoPlayer.isPrepared)
            {
                await Task.Yield();
            }
            m_Screen.texture = m_VideoPlayer.texture;
            return true;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Init()
        {
            if (m_Screen == null || m_VideoPlayer == null)
            {
                Debug.LogError("Component NULL!");
            }
            return await PrepareVideo();
        }

        /// <summary>
        /// 動画を再生
        /// </summary>
        /// <param name="isLoop">反復再生</param>
        /// <returns>動画再生に成功したら、Trueを返す</returns>
        public async Task<bool> Play(bool isLoop)
        {
            if (m_VideoPlayer == null) return false;
            if (!m_VideoPlayer.isPrepared) return false;
            m_VideoPlayer.isLooping = isLoop;
            m_VideoPlayer.Play();
            return true;
        }

        /// <summary>
        /// 動画再生を注視する
        /// </summary>
        /// <returns>動画再生注視にしたら、Trueを返す</returns>
        public async Task<bool> Stop()
        {
            if (m_VideoPlayer == null)  return false; 
            if (!m_VideoPlayer.isPrepared) return false;
            m_VideoPlayer.Stop();
            return true;
        }
    }
}
