using UnityEngine;
using UnityEngine.UI;

namespace _02.Scripts.Slot
{
    public class SlotUnit : Poolable
    {
        [SerializeField] private RawImage _rawImage = null;

        public void SetupSlotUnit(Texture2D texture)
        {
            if (_rawImage != null)
                _rawImage.texture = texture;
        }
    }
}
