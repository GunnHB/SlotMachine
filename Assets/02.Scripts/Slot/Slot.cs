using System;
using System.Collections.Generic;
using _02.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

using _02.Scripts.UI;

using DG.Tweening;

namespace _02.Scripts.Slot
{
    
    public class Slot : MonoBehaviour
    {
        private static readonly string Prefab_Name_SlotUnit = "SlotUnit";
        
        [SerializeField] private SlotData _slotData = null;
        
        [SerializeField] private ScrollRect _scroll = null;
        [SerializeField] private UIButton _button = null;
        
        private List<SlotUnit> _unitList = new List<SlotUnit>();
        
        private bool _bIsSpinning = false;

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
        }

        private void SetupSlotUnit()
        {
            // if (_slotData == null || _slotUnitPrefab == null)
            //     return;
            //
            // foreach (var item in _slotData.TextureList)
            // {
            //     SlotUnit unit = Instantiate(_slotUnitPrefab,  _scroll.content.transform);
            //     if (unit != null)
            //     {
            //         unit.SetupSlotUnit(item);
            //         
            //         _unitList.Add(unit);
            //     }
            // }
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
            _button.Text.SetText("Stop!");
            _bIsSpinning = true;
        }

        private void StopSpin()
        {
            _button.Text.SetText("Start!");
            _bIsSpinning = false;
        }
    }
} 