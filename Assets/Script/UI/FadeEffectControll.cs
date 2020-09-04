using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicGame.UI
{
    public class FadeEffectControll : MonoBehaviour
    {
        /// <summary>
        /// ShapeありのAlpha画像
        /// </summary>
        [SerializeField] private Image m_mainFade;

        private readonly Color m_alphaOn = new Color(1f, 1f, 1f, 1f);
        private readonly Color m_alphaOff = new Color(1f, 1f, 1f, 0f);

        private readonly float m_checkValue = 0.025f;

        /// <summary>
        /// Fade-In (暗 to 明) Coroutine
        /// </summary>
        /// <param name="duration">演出時間</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator FadeIn(float duration)
        {
            m_mainFade.color = m_alphaOn;
            //m_subFade.color = m_alphaOn;
            var val = 0f;
            if (!m_mainFade.gameObject.activeSelf)
            {
                m_mainFade.gameObject.SetActive(true);
            }
            while (val < duration)
            {
                var nowColor = m_mainFade.color;
                yield return new WaitForFixedUpdate();
                val += Time.fixedDeltaTime;
                m_mainFade.color = Color.Lerp(nowColor, m_alphaOff, Time.fixedDeltaTime);

                if (m_mainFade.color.a < m_checkValue)
                {
                    m_mainFade.color = m_alphaOff;
                }
            }
            m_mainFade.gameObject.SetActive(false);
            yield return null;
        }

        /// <summary>
        /// Fade-In (明 to 暗) Coroutine
        /// </summary>
        /// <param name="duration">演出時間</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator FadeOut(float duration)
        {
            m_mainFade.color = m_alphaOff;
            //m_subFade.color = m_alphaOff;
            var val = 0f;

            if (!m_mainFade.gameObject.activeSelf)
            {
                m_mainFade.gameObject.SetActive(true);
            }

            while (val < duration)
            {
                var nowColor = m_mainFade.color;
                yield return new WaitForFixedUpdate();
                val += Time.fixedDeltaTime;
                m_mainFade.color = Color.Lerp(nowColor, m_alphaOn, Time.fixedDeltaTime);

                if (m_mainFade.color.a > 1f - m_checkValue)
                {
                    m_mainFade.color = m_alphaOn;
                }
            }
            yield return null;
        }
    }
}