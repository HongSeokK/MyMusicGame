using UnityEngine;
using MusicGame.UI.GameMain;
using MusicGame.UI;
using System.Threading.Tasks;
using System.Collections;
using MusicGame.DB;

namespace MusicGame.Scene.GameMain
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private FadeEffectControll m_FadeEffectControll;

        [SerializeField]
        private MoviePlayControl m_MoviePlayControl;

        [SerializeField]
        private GameMainManager m_GameMainManager;

        [SerializeField]
        private MusicPlayControl m_MusicPlayControl;

        private async void Awake()
        {
            await DBManager.Initialize();
            var waitTime = await m_GameMainManager.Init();
            await Task.WhenAll(m_MoviePlayControl.Init(), m_MusicPlayControl.Init());
            m_GameMainManager.StartGame();
            StartCoroutine(MovieMusicPlayWithWait(waitTime));
        }

        private IEnumerator MovieMusicPlayWithWait(float time)
        {
            Debug.Log(time);
            m_MoviePlayControl.Play(true);
            yield return StartCoroutine(m_FadeEffectControll.FadeIn(time));
            m_MusicPlayControl.Play(false); 
        }
    }
}