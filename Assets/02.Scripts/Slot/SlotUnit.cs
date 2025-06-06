using UnityEngine;
using UnityEngine.UI;

namespace _02.Scripts.Slot
{
    public class SlotUnit : Poolable
    {
        [SerializeField] private RawImage _rawImage = null;

        private SlotData.SlotItem _currentSlotItem = null;
        
        public SlotData.SlotItem GetSlotItem() => _currentSlotItem;

        public void SetSlotItem(SlotData.SlotItem item)
        {
            _currentSlotItem = item;

            if (_rawImage != null)
                _rawImage.texture = item._itemTexture;
        }
    }
}
