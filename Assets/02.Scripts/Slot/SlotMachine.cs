using System;
using System.Collections.Generic;
using _02.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

using _02.Scripts.UI;

using DG.Tweening;
using Sirenix.OdinInspector;

namespace _02.Scripts.Slot
{
    
    public class SlotMachine : RecyclableVerticalScrollView<SlotData.SlotItem>
    {
        [Title("Data")]
        [SerializeField] private SlotData _slotData = null;
        [SerializeField] private float _spinSpeed = 500f;
        
        [Title("UI")]
        [SerializeField] private UIButton _button = null;
        
        private List<SlotData.SlotItem> _itemList = new List<SlotData.SlotItem>();
        
        private bool _bIsSpinning = false;
        private int _currSlotIndex = 0;

        private Tween _spinTween = null;

        private void Start()
        {
            SetupSlotUnit();
            
            if (_button != null)
            {
                _button.Text.SetText("Start!");
                _button.onClick.AddListener(OnClickButton);
            }
        }

        private void OnDestroy()
        {
            if(_button != null)
                _button.onClick.RemoveAllListeners();
            
            _spinTween?.Kill();
        }

        private void SetupSlotUnit()
        {
            _itemList.Clear();
            
            foreach (var item in _slotData.SlotItems)
                _itemList.Add(item);
            
            Init(_itemList);
        }

        private void OnClickButton()
        {
            if(_bIsSpinning)
                StopSpin();
            else
                StartSpin();
        }

        private void StartSpin()
        {
        }

        private void StopSpin()
        {
            
        }
    }
} 