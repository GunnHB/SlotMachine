using System;
using System.Collections.Generic;

using UnityEngine;

using _02.Scripts.UI;

using TMPro;

using DG.Tweening;

namespace _02.Scripts.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private TMP_Dropdown _dropdown = null;
        [SerializeField] private List<Slot.SlotMachine> _slotList = new List<Slot.SlotMachine>();
        [SerializeField] private UIEscapePanel _escapePanel = null;

        private bool _activatedPanel = false;

        protected override void Awake()
        {
            base.Awake();
            
            InitDropdown();
        }

        private void Start()
        {
            if (_escapePanel != null)
                _escapePanel.OnRequestClosePanel += CloseEscapePanel;
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

        private void OnEscape()
        {
            if (_escapePanel == null)
                return;

            if (_activatedPanel == true)
            {
                _escapePanel.OnRequestClosePanel?.Invoke();
                return;
            }

            Sequence sequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    _activatedPanel = true;
                    
                    _escapePanel.Popup.transform.localScale = Vector3.zero;
                    _escapePanel.Popup.GetComponent<CanvasGroup>().alpha = 0;

                    _escapePanel.gameObject.SetActive(true);
                })
                .Append(_escapePanel.Popup.transform.DOScale(1f, .15f).SetEase(Ease.OutBounce))
                .Join(_escapePanel.Popup.GetComponent<CanvasGroup>().DOFade(1f, .15f));
        }

        private void CloseEscapePanel()
        {
            Sequence sequence = DOTween.Sequence()
                .Append(_escapePanel.Popup.transform.DOScale(0f, .15f).SetEase(Ease.InBounce))
                .Join(_escapePanel.Popup.GetComponent<CanvasGroup>().DOFade(0f, .15f))
                .OnComplete(() =>
                {
                    _escapePanel.gameObject.SetActive(false);
                    
                    _activatedPanel = false;
                });
        }
    }
}
