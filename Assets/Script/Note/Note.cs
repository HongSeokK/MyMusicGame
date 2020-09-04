using UnityEngine;
namespace MusicGame.Note
{
    /// <summary>
    /// ノートインタフェース
    /// </summary>
    public interface INote
    {
        /// <summary>
        /// タイプ ( 右、左 )
        /// </summary>
        NoteType Type { get; }

        /// <summary>
        /// 生成時間
        /// </summary>
        float CreateTime { get; }
    }

    /// <summary>
    /// ノートファクトリー
    /// </summary>
    public static class NoteFactory
    {
        public static INote Create(
            NoteType type,
            float createTime
            ) => new NoteImpl(
                type,
                createTime); 

        private sealed class NoteImpl : INote
        {
            /// <summary>
            /// タイプ
            /// </summary>
            public NoteType Type { get; }

            /// <summary>
            /// 生成時間
            /// </summary>
            public float CreateTime { get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="type"></param>
            /// <param name="createTime"></param>
            public NoteImpl(
                NoteType type,
                float createTime
                ) => (
                Type,
                CreateTime
                ) = (
                type,
                createTime
                );
        }
    }

    /// <summary>
    /// ノートタイプ
    /// </summary>
    public enum NoteType
    {
        /// <summary>
        /// 左
        /// </summary>
        Left = 0,
        /// <summary>
        /// 右
        /// </summary>
        Right = 1,
    }
}