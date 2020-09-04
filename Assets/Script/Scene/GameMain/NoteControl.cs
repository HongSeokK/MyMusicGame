using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MusicGame.Scene.GameMain.NoteControl
{
    public class NoteControl : MonoBehaviour
    {
        /// <summary>
        /// スタート支店
        /// </summary>
        [SerializeField]
        private GameObject m_Start;

        /// <summary>
        /// 右ノート
        /// </summary>
        [SerializeField]
        private GameObject m_Note;

        [SerializeField]
        private ComboScoreControl m_ComboScoreControl;

        /// <summary>
        /// 動いてるノートのリスト
        /// </summary>
        private List<GameObject> m_MovingNoteList;

        /// <summary>
        /// オブジェクトプーリングするためのキュー 
        /// </summary>
        private Queue<GameObject> m_NotePool;

        [SerializeField]
        private GameObject m_Center;

        private float m_noteSpeed = 1f;

        public void Init(float speed,int noteCounts)
        {
            m_NotePool = new Queue<GameObject>();
            m_MovingNoteList = new List<GameObject>();
            m_noteSpeed = speed;
            for (int i = 0; i < 20; i++)
            {
                var note = Instantiate(m_Note, m_Start.transform.position, m_Start.transform.rotation, m_Start.transform);
                m_NotePool.Enqueue(note);
                note.SetActive(false);
            }
            m_ComboScoreControl.Init(noteCounts);
        }


        /// <summary>
        /// ノート生成
        /// </summary>
        public void CreateNote()
        {
            var note = m_NotePool.Dequeue();
            note.SetActive(true);
            m_MovingNoteList.Add(note);
            StartCoroutine(MovingNote(note));
        }


        /// <summary>
        /// 左ノートを動かせる
        /// </summary>
        /// <param name="noteObj"></param>
        /// <returns></returns>
        private IEnumerator MovingNote(GameObject noteObj)
        {
            // 方向
            var dir = (m_Center.transform.position - noteObj.transform.position).normalized;

            // スピード計算 ( 生成された時に判定ボタンまでの距離で計算、 120で分けたら1秒、 * 倍速 ) 
            var speed = (Vector2.Distance(m_Center.transform.position, noteObj.transform.position) / 100f) * m_noteSpeed;
            // 判定ボタンを過ごしたのか確認
            var isChecking = false;
            var time = 0f;

            while (noteObj.active)
            {
                // 移動させる
                noteObj.transform.position += dir * speed;

                time += Time.deltaTime;
                // 最終点までの距離
                var dist = Vector2.Distance(noteObj.transform.position, m_Center.transform.position);
                //if (dist <= 0.05f) { Debug.Log(m_time);  Debug.Log(time); }

                if (dist <= 1f)
                {
                    isChecking = true;
                }
                if (isChecking)
                {
                    if (dist >= 1.1f)
                    {
                        DeleteNote(noteObj);

                        m_ComboScoreControl.ComboReset();
                        break;
                    }
                }
                yield return null;
            }
        }

        /// <summary>
        /// タップ判定
        /// </summary>
        public void SideDown()
        {
            foreach (var note in m_MovingNoteList)
            {
                var dist = Vector2.Distance(note.transform.position, m_Center.transform.position);

                if (dist <= 1f)
                {
                    switch (dist)
                    {
                        case var _ when dist <= 0.3f:
                            m_ComboScoreControl.ComboWithGrade(Common.SceneCommon.PERFECT_IMAGE_NAME);
                            break;
                        case var _ when dist <= 0.8f:
                            m_ComboScoreControl.ComboWithGrade(Common.SceneCommon.COOL_IMAGE_NAME);
                            break;
                        default:
                            m_ComboScoreControl.ComboWithGrade(Common.SceneCommon.GOOD_IMAGE_NAME);
                            break;
                    }
                    DeleteNote(note);
                    break;
                }
            }
        }

        /// <summary>
        /// 左ノートをリストから削除、キューに追加、位置初期化
        /// </summary>
        /// <param name="note"></param>
        private void DeleteNote(GameObject note)
        {
            m_NotePool.Enqueue(note);
            m_MovingNoteList.Remove(note);
            note.SetActive(false);
            note.transform.position = m_Start.transform.position;
        }

    }
}