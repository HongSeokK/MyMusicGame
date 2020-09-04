using UnityEngine;
using System;

namespace MusicGame.Score
{
    /// <summary>
    /// スコアーインタフェース
    /// </summary>
    public interface IScore
    {
        /// <summary>
        /// 全体スコアー
        /// </summary>
        int Score { get; }

        /// <summary>
        /// パーフェクト数
        /// </summary>
        int Perfect { get; }

        /// <summary>
        /// クール数
        /// </summary>
        int Cool { get; }

        /// <summary>
        /// グッド数
        /// </summary>
        int Good { get; }

        /// <summary>
        /// ミス数
        /// </summary>
        int Miss { get; }

        /// <summary>
        /// 最大コンボ
        /// </summary>
        int MaxCombo { get; }

        /// <summary>
        /// 等級
        /// </summary>
        ResultGrade ResultGrade { get; }
    }

    /// <summary>
    /// スコアーファクトリ
    /// </summary>
    public static class ScoreFactory
    {
        public static IScore Create(
                int score,
                int perfect,
                int cool,
                int good,
                int miss,
                int maxCombo,
                ResultGrade resultGrade)
            {

            var result = new ScoreImpl(
                    score,
                    perfect,
                    cool,
                    good,
                    miss,
                    maxCombo,
                    resultGrade);

            lazyScore = new Lazy<IScore>(() => result);

            return result;
            }

        private sealed class ScoreImpl : IScore
        {
            /// <summary>
            /// 全体スコアー
            /// </summary>
            public int Score { get; }

            /// <summary>
            /// パーフェクト数
            /// </summary>
            public int Perfect { get; }

            /// <summary>
            /// クール数
            /// </summary>
            public int Cool { get; }

            /// <summary>
            /// グッド数
            /// </summary>
            public int Good { get; }

            /// <summary>
            /// ミス数
            /// </summary>
            public int Miss { get; }

            /// <summary>
            /// 最大コンボ
            /// </summary>
            public int MaxCombo { get; }

            /// <summary>
            /// 等級
            /// </summary>
            public ResultGrade ResultGrade { get; }

            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="score"></param>
            /// <param name="perfect"></param>
            /// <param name="cool"></param>
            /// <param name="good"></param>
            /// <param name="miss"></param>
            public ScoreImpl(
                int score,
                int perfect,
                int cool,
                int good,
                int miss,
                int maxCombo,
                ResultGrade resultGrade
                ) => (
                Score,
                Perfect,
                Cool,
                Good,
                Miss,
                MaxCombo,
                ResultGrade
                ) = (
                score,
                perfect,
                cool,
                good,
                miss,
                maxCombo,
                resultGrade
                );
        }

        public static Lazy<IScore> lazyScore;
    }

    public enum ResultGrade
    {
        SS = 0,
        S = 1,
        A ,
        B,
        C,
        D,
    }

    public enum ResultGradeScore
    { 
        SS = 980000,
        S = 950000,
        A = 900000,
        B = 800000,
        C = 700000,
        D = 600000,
    }
}