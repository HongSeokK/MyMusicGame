using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicGame.UI.GameMain
{
    public class GuageControl : MonoBehaviour
    {
        [SerializeField] private Slider m_guage;

        private float m_nowFloatValue = 0f;

        private int m_nowIntValue = 0;


        private void Awake()
        {
            if(m_guage == null)
            {
                Debug.LogError("Guage is NULL");
                return;
            }
        }

        private void Start()
        {
            if (m_guage == null)
            {
                return;
            }
            m_guage.value = 0;
            m_nowFloatValue = 0f;
            m_nowIntValue = 0;
        }

        /// <summary>
        /// ゲージ値を更新する(セットした値で上書き)
        /// </summary>
        /// <param name="fNewVal">ゲージのパーセンテージ値</param>
        public void SetValue(float fNewVal)
        {
            m_guage.value = fNewVal;
        }

        /// <summary>
        /// ゲージ値を更新する(セットした値で上書き)
        /// </summary>
        /// <param name="newVal">ゲージに反映する値</param>
        /// <param name="maxVal">全体値</param>
        public void SetValue(int newVal, int maxVal)
        {
            m_guage.value = Mathf.Clamp(newVal, 0, maxVal);
        }

        /// <summary>
        /// 増加/減少させたい分の値だけをゲージに反映する
        /// </summary>
        /// <param name="fVal">変動させたい分だけの値</param>
        public void ChangeeValue(float fVal)
        {
            m_nowFloatValue += fVal;
            m_guage.value = Mathf.Clamp01(m_nowFloatValue);
        }


        /// <summary>
        /// 増加/減少させたい分の値だけをゲージに反映する
        /// </summary>
        /// <param name="val"></param>
        public void ChangeeValue(int val, int maxVal)
        {
            m_nowIntValue += val;
            m_guage.value = Mathf.Clamp(m_nowIntValue, 0, maxVal);
        }
    }
}