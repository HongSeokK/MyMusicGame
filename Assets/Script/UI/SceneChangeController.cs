using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using SceneInfo = MusicGame.Common.SceneCommon.SceneInfo;

namespace MusicGame.Scene
{
    public class SceneChangeController : MonoBehaviour
    {
        public static string nextScene;
        [SerializeField] private Slider m_progressBar;

        private void Start()
        {
            m_progressBar.value = 0f;
            StartCoroutine(LoadScene());
        }

        public static void LoadScene(SceneInfo next)
        {
            nextScene = $"Scenes/{next}";
            SceneManager.LoadScene($"Scenes/{SceneInfo.LoadingScene}");
        }

        private IEnumerator LoadScene()
        {
            yield return null;

            var op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;
            float timer = 0.0f;
            while (!op.isDone)
            {
                yield return null;

                timer += Time.deltaTime;
                if (op.progress < 0.9f)
                {
                    m_progressBar.value = Mathf.Lerp(m_progressBar.value, op.progress, timer);
                    if (m_progressBar.value >= op.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                    m_progressBar.value = Mathf.Lerp(m_progressBar.value, 1f, timer);
                    if (m_progressBar.value == 1.0f)
                    {
                        yield return new WaitForSeconds(0.5f);
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}