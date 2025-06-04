using UnityEngine;

using System.Collections.Generic;

using TMPro;

namespace _02.Scripts.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        private TMP_Dropdown _dropdown = null;
        private List<Slot.Slot> _slotList = new List<Slot.Slot>();

        public void SetupDropdown(TMP_Dropdown dropdown)
        {
            if (dropdown != null)
                _dropdown = dropdown;
        }

        public void SetupSlotList(List<Slot.Slot> slotList)
        {
            _slotList = slotList;
        }

        private void Start()
        {
            if (_dropdown == null)
                return;
            
            _dropdown.onValueChanged.AddListener(OnValueChangedDropdown);
            _dropdown.onValueChanged.Invoke(0);
        }

        private void OnValueChangedDropdown(int value)
        {
            foreach (var item in _slotList)
                item.gameObject.SetActive(false);
            
            for (int index = 0; index < value + 1; ++index)
                _slotList[index].gameObject.SetActive(true);
        }
    }
}
