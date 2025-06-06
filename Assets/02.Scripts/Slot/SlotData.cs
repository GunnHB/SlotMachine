using System;
using UnityEngine;

using UnityEngine.UI;

using System.Collections.Generic;

namespace _02.Scripts.Slot
{
    [CreateAssetMenu(fileName = "NewSlotData", menuName = "Scriptable Object Asset/SlotData")]
    public class SlotData : ScriptableObject
    {
        [Serializable]
        public class SlotItem
        {
            public Texture2D _itemTexture = null;
            public string _exerciseName = string.Empty;
        }

        [SerializeField] private SlotItem[] _slotItems = null;

        public SlotItem[] SlotItems => _slotItems;
    }
}
