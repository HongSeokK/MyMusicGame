using System.Collections;
using System.Collections.Generic;
using MusicGame.Common;
using UnityEngine;

namespace MusicGame.Scene
{
    public sealed class InitSceneFrame : MonoBehaviour
    {
        private void Awake()
        {

        }

        private void InitCameraRect()
        {
            var mainCamera = Camera.main;
            var rect = mainCamera.rect;
            var scaleHeight = ((float)Screen.width / Screen.height) / ((float)SceneCommon.WIDTH_ASPECT / SceneCommon.WIDTH_ASPECT);
            var scaleWidth = 1f / scaleHeight;

            if(scaleHeight < 1)
            {
                rect.height = scaleHeight;
                rect.y = (1f - scaleHeight) / 2f;
            }
            else
            {
                rect.width = scaleWidth;
                rect.x = (1f - scaleWidth) / 2f;
            }

            mainCamera.rect = rect;
        }
    }
}