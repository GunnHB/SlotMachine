using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

namespace _02.Scripts.Slot
{
    [CreateAssetMenu(fileName = "NewSlotData", menuName = "Scriptable Object Asset/SlotData")]
    public class SlotData : ScriptableObject
    {
        [SerializeField] private List<Texture2D> _textureList = new List<Texture2D>();
        
        public List<Texture2D> TextureList => _textureList;
    }
}
