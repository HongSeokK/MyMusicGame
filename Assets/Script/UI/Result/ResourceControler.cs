using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicGame.UI.ResultScene
{
    public class ResourceControler : MonoBehaviour
    {
        [SerializeField] private List<Sprite> m_sprites;
        private Dictionary<string, Sprite> m_spriteMapper;

        public void Init()
        {
            m_spriteMapper = new Dictionary<string, Sprite>();
            foreach (var sprite in m_sprites)
            {
                m_spriteMapper.Add(sprite.name, sprite);
            }
        }

        public Sprite GetSprite(string key)
        {
            Sprite result;
            if(m_spriteMapper.TryGetValue(key, out result))
            {
                return result;
            }
            Debug.LogError($"Result = Null by {key}");
            return null;
        }
    }
}