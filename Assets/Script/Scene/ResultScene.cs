using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneInfo = MusicGame.Common.SceneCommon.SceneInfo;
using MusicGame.UI.ResultScene;

namespace MusicGame.Scene.ResultScene
{
    public class ResultScene : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        [SerializeField] private ResourceControler m_resourceControler;

        [SerializeField] private GradeResult m_grade;
        [SerializeField] private CountDisplay m_score;
        [SerializeField] private CountDisplay m_perfect;
        [SerializeField] private CountDisplay m_cool;
        [SerializeField] private CountDisplay m_good;
        [SerializeField] private CountDisplay m_miss;
        [SerializeField] private CountDisplay m_maxCombo;

        private void Start()
        {
            if (Score.ScoreFactory.lazyScore == null)
            {
                Score.ScoreFactory.Create(111, 222, 333, 444, 555, 666,Score.ResultGrade.D);
            }
            m_resourceControler.Init();

            m_grade.Init(m_resourceControler);
            m_score.Init(m_resourceControler);
            m_perfect.Init(m_resourceControler);
            m_cool.Init(m_resourceControler);
            m_good.Init(m_resourceControler);
            m_miss.Init(m_resourceControler);
            m_maxCombo.Init(m_resourceControler);

            m_grade.SetValue(Score.ScoreFactory.lazyScore.Value.ResultGrade.ToString().ToLower());
            m_score.SetValue(Score.ScoreFactory.lazyScore.Value.Score);
            m_perfect.SetValue(Score.ScoreFactory.lazyScore.Value.Perfect);
            m_cool.SetValue(Score.ScoreFactory.lazyScore.Value.Cool);
            m_good.SetValue(Score.ScoreFactory.lazyScore.Value.Good);
            m_miss.SetValue(Score.ScoreFactory.lazyScore.Value.Miss);
            m_maxCombo.SetValue(Score.ScoreFactory.lazyScore.Value.MaxCombo);
        }

        public void SceneProcess()
        {
            m_button.interactable = false;
            SceneChangeController.LoadScene(SceneInfo.StartScene);
        }
    }
}