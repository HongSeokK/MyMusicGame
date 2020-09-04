using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MusicGame.Note;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MusicGame.DB;
using MusicGame.Score;
using MusicGame.UI.GameMain;
using MusicGame.UI;
using NoteController = MusicGame.Scene.GameMain.NoteControl.NoteControl;
using ComboController = MusicGame.Scene.GameMain.NoteControl.ComboScoreControl;
using SoundType = MusicGame.Common.SceneCommon.SoundEffect;

namespace MusicGame.Scene.GameMain
{
    public class GameMainManager : MonoBehaviour
    {
        [SerializeField]
        private NoteController LeftControl;

        [SerializeField]
        private NoteController RightControl;

        [SerializeField]
        private ComboController ComboControl;

        /// <summary>
        /// ノートリスト
        /// </summary>
        private List<INote> m_Notes;

        /// <summary>
        /// キャンバスのグラピックレイキャスター
        /// </summary>
        [SerializeField]
        private GraphicRaycaster graphicRaycaster;

        /// <summary>
        /// サウンドエフェクトコントロール
        /// </summary>
        [SerializeField]
        private SoundEffectControl m_soundEffectControl;

        /// <summary>
        /// Fadeエフェクトコントロール
        /// </summary>
        [SerializeField]
        private FadeEffectControll m_fadeEffectControl;

        /// <summary>
        /// ノート倍速
        /// </summary>
        private float m_noteSpeed;

        private float m_time;

        private bool m_IsStarted;

        private void Update()
        {
            // マウス判定
            if (Input.GetMouseButtonDown(0))
            {
                PointerDown();
            }

            // ターチ判定
            if (Input.touchCount > 0)
            {
                for (int touch = 0; touch < Input.touchCount; touch++)
                {
                    var nTouch = Input.GetTouch(touch);

                    if (nTouch.phase == TouchPhase.Began)
                    {
                        TouchDown(touch);
                    }
                }
            }

            if(m_IsStarted)
            {
                m_time += Time.deltaTime;
            }
        }

        private void InitGameInfo()
        {
            m_Notes = new List<INote>();
            m_IsStarted = false;
            m_noteSpeed = 1f;

            var noteTable = DBManager.SQLConnect.Table<NoteTable>().AsEnumerable().OrderBy(f => f.ID);

            var noteCounts = noteTable.Count();

            LeftControl.Init(m_noteSpeed,noteCounts);
            RightControl.Init(m_noteSpeed,noteCounts);

            var FirstNoteTime = 5f;

            foreach (var noteInfo in noteTable)
            {

                var noteCreateTime = noteInfo.CreateTime / 1000f;
                var createTime = FirstNoteTime + noteCreateTime - m_noteSpeed;
                switch (noteInfo.Type)
                {
                    case "Left":
                        m_Notes.Add(NoteFactory.Create(NoteType.Left, createTime));
                        break;
                    case "Right":
                        m_Notes.Add(NoteFactory.Create(NoteType.Right, createTime));
                        break;
                }
            }
        }
        /// <summary>
        /// ベタガキ
        /// </summary>
        public async Task<float> Init()
        {
            InitGameInfo();
            var FirstNoteTime = 5f;


            return FirstNoteTime;
        }

        public void StartGame()
        {
            m_IsStarted = true;
            StartCoroutine(CreateNotes());
        }

        public void EndGame()
        {
            SceneChangeController.LoadScene(Common.SceneCommon.SceneInfo.ResultScene);
        }

        /// <summary>
        /// 指定時間でノートを生成する
        /// </summary>
        /// <returns></returns>
        private IEnumerator CreateNotes()
        {
            foreach (var note in m_Notes)
            {
                var time = note.CreateTime - m_time;
                yield return new WaitForSeconds(time);
                // 生成して動かせる
                switch (note.Type)
                {
                    case NoteType.Left:
                        LeftControl.CreateNote();
                        //CreateLeftNote();
                        break;
                    case NoteType.Right:
                        RightControl.CreateNote();
                        break;
                }
            }
            yield return StartCoroutine(m_fadeEffectControl.FadeOut(5f));

            ComboControl.EndGame();

            m_soundEffectControl.Play(SoundType.Finish);
            EndGame();
        }

        /// <summary>
        /// マウスポインタが押された時
        /// </summary>
        private void PointerDown()
        {
            // グラピックレイキャスターで右、左どっちをターチしたのか確認、判定適用
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(ped, results);
            foreach (var s in results)
            {
                switch (s.gameObject.tag)
                {
                    case Common.SceneCommon.RIGHT_SIDE_TAG:
                        RightControl.SideDown();
                        break;
                    case Common.SceneCommon.LEFT_SIDE_TAG:
                        LeftControl.SideDown();
                        break;
                }
            }
        }

        /// <summary>
        /// ターチされた時
        /// </summary>
        /// <param name="num"></param>
        private void TouchDown(int num)
        {
            // グラピックレイキャスターで右、左どっちをターチしたのか確認、判定適用
            var ped = new PointerEventData(null);
            ped.position = Input.touches[num].position;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(ped, results);
            foreach (var s in results)
            {
                switch (s.gameObject.tag)
                {
                    case Common.SceneCommon.RIGHT_SIDE_TAG:
                        RightControl.SideDown();
                        break;
                    case Common.SceneCommon.LEFT_SIDE_TAG:
                        LeftControl.SideDown();
                        break;
                }
            }
        }

    }
}