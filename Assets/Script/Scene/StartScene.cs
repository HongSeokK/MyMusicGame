using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneInfo = MusicGame.Common.SceneCommon.SceneInfo;

namespace MusicGame.Scene.StartScene
{
    public class StartScene : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        public void SceneProcess()
        {
            m_button.interactable = false;
            SceneChangeController.LoadScene(SceneInfo.GameMain);
        }
    }
}