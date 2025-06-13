using UnityEngine;
using UnityEngine.UI;

namespace _02.Scripts.Slot
{
    public class Slot : UIRecyclableScrollSlot<SlotData.SlotItem>
    {
        [SerializeField] private RawImage _rawImage = null;

        private SlotData.SlotItem _currentSlotItem = null;
        
        public SlotData.SlotItem GetSlotItem() => _currentSlotItem;

        public override void Init()
        {
        }

        public override void UpdateSlot(SlotData.SlotItem item)
        {
            if (_rawImage != null)
                _rawImage.texture = item._itemTexture;
        }
    }
}
