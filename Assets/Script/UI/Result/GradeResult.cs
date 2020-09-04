using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ResourceName = MusicGame.Common.SceneCommon;

namespace MusicGame.UI.ResultScene
{
    public class GradeResult : MonoBehaviour
    {
        [SerializeField] private Image m_grade;
        private ResourceControler m_resourceControl;

        public void Init(ResourceControler resourceControl)
        {
            m_resourceControl = resourceControl;
        }

        public void SetValue(string value)
        {
            m_grade.sprite = m_resourceControl.GetSprite($"{ResourceName.GRADE_NAME}{value}");
        }
    }
}