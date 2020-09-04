using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ResourceName = MusicGame.Common.SceneCommon;

namespace MusicGame.UI.ResultScene
{
    public class TypeCountDisplay : CountDisplay
    {
        [SerializeField] private List<Image> m_scores;

        public override void SetValue(int val)
        {
            var rawVal = val.ToString("0000");
            var tmpArr = rawVal.ToCharArray();
            for (var i = 0; i < tmpArr.Length; i++)
            {
                m_scores[i].sprite = m_resourceControl.GetSprite($"{ResourceName.NUMBER_NAME}{tmpArr[i]}");
            }
        }
    }
}