using System;
using UnityEngine;

using System.Collections.Generic;

using TMPro;

namespace _02.Scripts.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private TMP_Dropdown _dropdown = null;
        [SerializeField] private List<Slot.SlotMachine> _slotList = new List<Slot.SlotMachine>();

        protected override void Awake()
        {
            base.Awake();
            
            InitDropdown();
        }

        private void InitDropdown()
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

        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveAllListeners();
        }
    }
}
