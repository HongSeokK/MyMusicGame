using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicGame.UI.ResultScene
{
    /// <summary>
    /// カウントを表示させるUIの親クラス
    /// </summary>
    public abstract class CountDisplay : MonoBehaviour
    {
        protected ResourceControler m_resourceControl;

        public void Init(ResourceControler resourceControl)
        {
            m_resourceControl = resourceControl;
        }
        /// <summary>
        /// 小クラスで使うスクリプト
        /// </summary>
        /// <param name="val">Score Value</param>
        public abstract void SetValue(int val);
    }
}
