using UnityEngine;

namespace MusicGame.UI
{
    /// <summary>
    /// タップエフェクトを発生させる
    /// </summary>
    public sealed class EffectGenerator : MonoBehaviour
    {
        /// <summary>
        /// 生成したエフェクトの生存時間
        /// </summary>
        private const float EFFECT_LIFE_TIME = 2.5f;

        /// <summary>
        /// 端末横向き時のカメラのサイズ
        /// </summary>
        private const int LANDSPACE_CAMERA_SIZE = 250;

        /// <summary>
        /// 端末縦向き時のカメラのサイズ
        /// </summary>
        private const int PORTRAIT_CAMERA_SIZE = 500;

        /// <summary>
        /// パーティクルの描画カメラ
        /// </summary>
        [SerializeField] private Camera m_camera;

        /// <summary>
        /// 通常エフェクト用プレハブ
        /// </summary>
        [SerializeField] private GameObject m_normalEffectPrefab;

        /// <summary>
        /// 交流エフェクト用プレハブ
        /// </summary>
        [SerializeField] private GameObject m_heartEffectPrefab;

        /// <summary>
        /// 生成可能判定
        /// </summary>
        private bool m_isEnable = true;

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                CreateEffect(Input.mousePosition);
            }
#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    CreateEffect(touch.position);
                }
            }
#endif
        }

        /// <summary>
        /// 端末向きの変更
        /// </summary>
        /// <param name="isLandspace">true : 横向き false : 縦向き</param>
        public void ChangeOrientation(bool isLandspace)
        {
            m_camera.orthographicSize = isLandspace ? LANDSPACE_CAMERA_SIZE : PORTRAIT_CAMERA_SIZE;
        }

        /// <summary>
        /// オイラー角の設定
        /// </summary>
        /// <param name="eulerAngles">オイラー角</param>
        public void SetEulerAngles(Vector3 eulerAngles) => m_camera.transform.localEulerAngles = eulerAngles;

        /// <summary>
        /// エフェクトの生成
        /// </summary>
        /// <param name="pos">入力座標</param>
        private void CreateEffect(Vector3 pos)
        {
            // 生成エフェクト未設定時は生成を行わない
            if (m_normalEffectPrefab == null) return;

            // エフェクト生成無効時は生成を行わない
            if (!m_isEnable) return;

            var effect = Instantiate(m_normalEffectPrefab, m_camera.ScreenToWorldPoint(pos), Quaternion.identity);
            effect.transform.SetParent(transform);
            effect.transform.localScale = Vector3.one;

            Destroy(effect, EFFECT_LIFE_TIME);
        }

        /// <summary>
        /// エフェクトの生成
        /// </summary>
        /// <param name="pos">入力座標</param>
        public void CreateComboEffect(Vector3 pos)
        {
            // 生成エフェクト未設定時は生成を行わない
            if (m_heartEffectPrefab == null) return;

            // エフェクト生成無効時は生成を行わない
            if (!m_isEnable) return;

            var effect = Instantiate(m_heartEffectPrefab, pos, Quaternion.identity);
            effect.transform.SetParent(transform);
            effect.transform.localScale = Vector3.one;

            Destroy(effect, EFFECT_LIFE_TIME);
        }

        /// <summary>
        /// 表示している全てのエフェクトを非表示
        /// </summary>
        public void InActiveEffect()
        {
            foreach (Transform effect in transform)
            {
                effect.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// エフェクト生成を有効
        /// </summary>
        public void EnableCreateEffect() => m_isEnable = true;

        /// <summary>
        /// エフェクト生成を無効
        /// </summary>
        public void DisableCreateEffect() => m_isEnable = false;
    }
}
