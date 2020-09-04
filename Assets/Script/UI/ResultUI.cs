using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MusicGame.Score;

namespace MusicGame.UI.ResultScene
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private Text m_totalScore;
        [SerializeField] private Text m_perfact;
        [SerializeField] private Text m_cool;
        [SerializeField] private Text m_good;
        [SerializeField] private Text m_miss;
        [SerializeField] private Text m_maxCombo;

        private void Start()
        {
            m_totalScore.text = ScoreFactory.lazyScore.Value.Score.ToString();
            m_perfact.text = ScoreFactory.lazyScore.Value.Perfect.ToString();
            m_cool.text = ScoreFactory.lazyScore.Value.Cool.ToString();
            m_good.text = ScoreFactory.lazyScore.Value.Good.ToString();
            m_miss.text = ScoreFactory.lazyScore.Value.Miss.ToString();
            m_maxCombo.text = ScoreFactory.lazyScore.Value.MaxCombo.ToString();
        }
    }
}