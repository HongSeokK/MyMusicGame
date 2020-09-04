using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicGame.Common
{
    public static class SceneCommon
    {
        /// <summary>
        /// Width 、Height 10 / 16 で設定
        /// </summary>
        public const float WIDTH_ASPECT = 16;
        public const float HEIGHT_ASPECT = 10;

        /// <summary>
        /// グレートテキスト
        /// </summary>
        public const string PERFECT_IMAGE_NAME = "judge_perfect";

        /// <summary>
        /// クールテキスト
        /// </summary>
        public const string COOL_IMAGE_NAME = "judge_cool";

        /// <summary>
        /// グッドテキスト
        /// </summary>
        public const string GOOD_IMAGE_NAME = "judge_good";

        /// <summary>
        /// ミステキスト
        /// </summary>
        public const string MISS_IMAGE_NAME = "judge_miss";

        /// <summary>
        /// 左側のタグ
        /// </summary>
        public const string LEFT_SIDE_TAG = "left";

        /// <summary>
        /// 右側のタグ
        /// </summary>
        public const string RIGHT_SIDE_TAG = "right";

        public const string JUDGE_PATH = "JudgeImages/";

        public const string NUMBER_PATH = "ComboNumbers/";

        public const string RESULT_PATH = "Result/";

        public const string NUMBER_NAME = "number_";

        public const string GRADE_NAME = "result_";

        public const string GRADE_A = "result_a";

        public const string GRADE_B = "result_b";

        public const string GRADE_C = "result_c";

        public const string GRADE_D = "result_d";

        public const string GRADE_S = "result_s";

        public const string GRADE_SS = "result_ss";

        public const int MAX_SCORE = 1000000;

        public enum SceneInfo
        {
            StartScene,
            LoadingScene,
            GameMain,
            ResultScene,
        }

        public enum SoundEffect
        {
            None,
            Miss,
            Normal,
            Perfect,
            Finish,
        }
    }

}
