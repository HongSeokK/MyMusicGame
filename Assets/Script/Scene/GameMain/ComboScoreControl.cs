using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using MusicGame.UI.GameMain;
using MusicGame.UI;
using MusicGame.Common;
using MusicGame.Score;
using SoundType = MusicGame.Common.SceneCommon.SoundEffect;

namespace MusicGame.Scene.GameMain.NoteControl
{
    public class ComboScoreControl : MonoBehaviour
    {
        /// <summary>
        /// コンボ文言イメージ
        /// </summary>
        [SerializeField]
        private Image m_ComboImage;

        /// <summary>
        /// コンボ表示用イメージ配列
        /// </summary>
        [SerializeField]
        private Image[] m_ComboNumbers;

        /// <summary>
        /// グレード文言イメージ
        /// </summary>
        [SerializeField]
        private Image m_GradeImage;

        /// <summary>
        /// ゲージコントローラー
        /// </summary>
        [SerializeField]
        private GuageControl GuageControl;

        /// <summary>
        /// エフェクトゼネレーター
        /// </summary>
        [SerializeField]
        private EffectGenerator m_effectGenerator;

        /// <summary>
        /// 判定オブジェクト
        /// </summary>
        [SerializeField]
        private GameObject Center;

        /// <summary>
        /// スコアー表示用イメージ配列
        /// </summary>
        [SerializeField]
        private Image[] m_ScoreNumbers;

        /// <summary>
        /// サウンドエフェクトコントロール
        /// </summary>
        [SerializeField]
        private SoundEffectControl m_soundEffectControl;

        /// <summary>
        /// Canvas(World座標への変換に必要)
        /// </summary>
        [SerializeField]
        private Canvas m_canvas;

        private Sprite m_PerfectSprite;
        private Sprite m_CoolSprite;
        private Sprite m_GoodSprite;
        private Sprite m_MissSprite;

        private Sprite[] m_NumberSprite;

        /// <summary>
        /// Center 画像のWorld Position
        /// </summary>
        private Vector3 m_centerButtonPos;

        /// <summary>
        /// コンボ
        /// </summary>
        private int m_Combo;
        private int m_MaxCombo;

        /// <summary>
        /// スコアー
        /// </summary>
        private float m_MaxScore;
        private float m_Score;

        private float m_PerfectScore;
        private float m_CoolScore;
        private float m_GoodScore;

        /// <summary>
        /// ゲージアップ情報
        /// </summary>
        private float m_PerfectGuageUp;
        private float m_CoolGuageUp;
        private float m_GoodGuageUp;
        private float m_MissGuageDown;

        /// <summary>
        /// 判定数
        /// </summary>
        private int m_Perfect;
        private int m_Cool;
        private int m_Good;
        private int m_Miss;

        private bool m_IsGradeImageApear;
        private bool m_IsComboImagesApear;

        /// <summary>
        /// グレード表示 Courutine
        /// </summary>
        private Coroutine m_GradeCourutine;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="noteCounts"></param>
        public void Init(int noteCounts)
        {
            m_PerfectSprite = Resources.Load<Sprite>(SceneCommon.JUDGE_PATH + SceneCommon.PERFECT_IMAGE_NAME);
            m_CoolSprite = Resources.Load<Sprite>(SceneCommon.JUDGE_PATH + SceneCommon.COOL_IMAGE_NAME);
            m_GoodSprite = Resources.Load<Sprite>(SceneCommon.JUDGE_PATH + SceneCommon.GOOD_IMAGE_NAME);
            m_MissSprite = Resources.Load<Sprite>(SceneCommon.JUDGE_PATH + SceneCommon.MISS_IMAGE_NAME);
            m_IsGradeImageApear = false;

            m_NumberSprite = new Sprite[10];

            var rect = Center.GetComponent<RectTransform>();
            var screenPos = RectTransformUtility.WorldToScreenPoint(m_canvas.worldCamera, rect.position);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, m_canvas.worldCamera, out m_centerButtonPos);

            for (int i = 0; i < 10; i++)
            {
                m_NumberSprite[i] = Resources.Load<Sprite>(SceneCommon.NUMBER_PATH + SceneCommon.NUMBER_NAME + i.ToString());
            }

            ComboNumberParseToImage(0);
            ScoreParseToImage(0);

            m_MaxScore = SceneCommon.MAX_SCORE;
            m_Score = 0;
            m_MaxCombo = 0;

            var Score = m_MaxScore / noteCounts;
            m_PerfectScore = Score * 1;
            m_CoolScore = Score * 0.8f;
            m_GoodScore = Score * 0.6f;

            m_PerfectGuageUp = (float)3 / noteCounts;
            m_CoolGuageUp = (float)2 / noteCounts;
            m_GoodGuageUp = (float)1 / noteCounts;
            m_MissGuageDown = -((float)1 / noteCounts);

            m_Perfect = 0;
            m_Cool = 0;
            m_Good = 0;
            m_Miss = 0;

        }

        /// <summary>
        /// スコアー集約
        /// </summary>
        /// <returns></returns>
        public IScore EndGame()
        {
            ResultGrade grade;

            switch (m_Score)
            {
                case float _ when m_Score >= (float)ResultGradeScore.SS:
                    grade = ResultGrade.SS;
                    break;
                case float _ when m_Score >= (float)ResultGradeScore.S:
                    grade = ResultGrade.S;
                    break;
                case float _ when m_Score >= (float)ResultGradeScore.A:
                    grade = ResultGrade.A;
                    break;
                case float _ when m_Score >= (float)ResultGradeScore.B:
                    grade = ResultGrade.B;
                    break;
                case float _ when m_Score >= (float)ResultGradeScore.C:
                    grade = ResultGrade.C;
                    break;
                default:
                    grade = ResultGrade.D;
                    break;
            }

            return ScoreFactory.Create((int)m_Score, m_Perfect, m_Cool, m_Good, m_Miss, m_MaxCombo, grade);
        }

        /// <summary>
        /// コンボリセット
        /// </summary>
        public void ComboReset()
        {
            if (m_MaxCombo < m_Combo) m_MaxCombo = m_Combo;
            m_Combo = 0;
            Debug.Log(m_MaxCombo);
            m_GradeImage.sprite = m_MissSprite;
            m_soundEffectControl.Play(SoundType.Miss);
            m_Miss++;
            if (m_GradeCourutine != null) StopCoroutine(m_GradeCourutine);
            m_GradeCourutine = StartCoroutine(ApearGradeImage());
            GuageControl.ChangeeValue(m_MissGuageDown);
            ComboNumberParseToImage(0);
            DisapearComboImages();
        }

        /// <summary>
        /// コンボアップ
        /// </summary>
        /// <param name="Grade"></param>
        public void ComboWithGrade(string Grade)
        {
            m_Combo += 1;
            var type = SoundType.None;
            switch (Grade)
            {
                case SceneCommon.PERFECT_IMAGE_NAME:
                    Debug.Log(m_PerfectGuageUp);
                    m_Perfect++;
                    m_Score += m_PerfectScore;
                    m_GradeImage.sprite = m_PerfectSprite;
                    GuageControl.ChangeeValue(m_PerfectGuageUp);
                    type = SoundType.Perfect;
                    break;
                case SceneCommon.COOL_IMAGE_NAME:
                    Debug.Log(m_CoolGuageUp);
                    m_Cool++;
                    m_Score += m_CoolScore;
                    m_GradeImage.sprite = m_CoolSprite;
                    GuageControl.ChangeeValue(m_CoolGuageUp);
                    type = SoundType.Normal;
                    break;
                case SceneCommon.GOOD_IMAGE_NAME:
                    Debug.Log(m_GoodGuageUp);
                    m_Good++;
                    m_Score += m_GoodScore;
                    m_GradeImage.sprite = m_GoodSprite;
                    GuageControl.ChangeeValue(m_GoodGuageUp);
                    type = SoundType.Normal;
                    break;
            }
            m_effectGenerator.CreateComboEffect(m_centerButtonPos);
            m_soundEffectControl.Play(type);

            if (m_GradeCourutine != null) StopCoroutine(m_GradeCourutine);
            m_GradeCourutine = StartCoroutine(ApearGradeImage());
            ScoreParseToImage((int)m_Score);
            ComboNumberParseToImage(m_Combo);
            if (!m_IsComboImagesApear) ApearComboImages();
        }

        /// <summary>
        /// コンボ表示
        /// </summary>
        private void ApearComboImages()
        {

            var color = new Color(255, 255, 255, 255);

            m_ComboImage.color = color;

            foreach (var image in m_ComboNumbers)
            {
                image.color = new Color(255, 255, 255, 255);
            }
            m_IsGradeImageApear = true;
        }

        /// <summary>
        /// コンボ数非表示
        /// </summary>
        private void DisapearComboImages()
        {
            var color = new Color(255, 255, 255, 0);
            m_ComboImage.color = color;
            foreach (var image in m_ComboNumbers)
            {
                image.color = color;
            }
            m_IsGradeImageApear = false;

        }

        /// <summary>
        /// コンボ関連イメージを表示するように変更
        /// </summary>
        private IEnumerator ApearGradeImage()
        {

            var color = new Color(255, 255, 255, 255);
            m_GradeImage.color = color;

            yield return new WaitForSeconds(1f);

            color = new Color(255, 255, 255, 0);
            m_GradeImage.color = color;
        }

        /// <summary>
        /// スコアーをイメージで変換
        /// </summary>
        /// <param name="score"></param>
        private void ScoreParseToImage(int score)
        {
            var str = score.ToString();

            var length = str.Length;

            for (int i = 0; i < length; i++)
            {
                var test = int.Parse(str.Substring(length - 1 - i, 1));

                m_ScoreNumbers[i].sprite = m_NumberSprite[test];
            }

            for (int i = length; i < m_ScoreNumbers.Length; i++)
            {
                m_ScoreNumbers[i].sprite = m_NumberSprite[0];
            }
        }

        /// <summary>
        /// コンボをイメージで変換
        /// </summary>
        /// <param name="number"></param>
        private void ComboNumberParseToImage(int number)
        {
            var str = number.ToString();

            var length = str.Length;

            for (int i = 0; i < length; i++)
            {
                var test = int.Parse(str.Substring(length - 1 - i, 1));

                m_ComboNumbers[i].sprite = m_NumberSprite[test];
            }

            for (int i = length; i < m_ComboNumbers.Length; i++)
            {
                m_ComboNumbers[i].sprite = m_NumberSprite[0];
            }
        }
    }
}
