using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicGame.UI.GameMain
{
    public class ScoreControl : MonoBehaviour
    {
        /// <summary>
        /// 後ろから1桁(0~9)
        /// </summary>
        [SerializeField] private Image m_units;
        /// <summary>
        /// 後ろから2桁(10~99)
        /// </summary>
        [SerializeField] private Image m_tens;
        /// <summary>
        /// 後ろから3桁(100~999)
        /// </summary>
        [SerializeField] private Image m_hundreds;
        /// <summary>
        /// 後ろから4桁(1000~9999)
        /// </summary>
        [SerializeField] private Image m_thousands;
        /// <summary>
        /// 後ろから5桁(10000~99999)
        /// </summary>
        [SerializeField] private Image m_tenTthousands;
        /// <summary>
        /// 後ろから6桁(100000~999999)
        /// </summary>
        [SerializeField] private Image m_hundredThousands;
        /// <summary>
        /// 後ろから7桁(1000000~9999999)
        /// </summary>
        [SerializeField] private Image millions;
    }
}