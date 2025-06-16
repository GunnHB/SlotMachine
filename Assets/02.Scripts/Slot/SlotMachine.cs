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
        [Title("Data")] [SerializeField] private SlotData _slotData = null;

        [Title("Spin Settings")] [SerializeField]
        private float _normalSpinSpeed = 1000f; // 일반 스핀 속도

        [SerializeField] private float _stopDistance = 3f; // 정지할 때 몇 칸 더 갈지 (슬롯 개수)
        [SerializeField] private float _stopDuration = 2f; // 정지 시간
        [SerializeField] private Ease _stopEase = Ease.OutQuart; // 정지 이징

        [Title("UI")] [SerializeField] private UIButton _button = null;

        [Title("Debug")] [SerializeField] private int _dataRepeatCount = 50;

        private List<SlotData.SlotItem> _expandedItemList = new List<SlotData.SlotItem>();

        private bool _bIsSpinning = false;
        private bool _bIsStopping = false;
        private Tween _spinTween = null;

        private void Start()
        {
            SetupSlotData();

            if (_button != null)
            {
                _button.Text.SetText("START");
                _button.onClick.AddListener(OnClickButton);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
                _button.onClick.RemoveAllListeners();

            _spinTween?.Kill();
        }

        private void SetupSlotData()
        {
            _expandedItemList.Clear();

            for (int repeat = 0; repeat < _dataRepeatCount; repeat++)
            {
                foreach (var item in _slotData.SlotItems)
                {
                    _expandedItemList.Add(item);
                }
            }

            Init(_expandedItemList);
        }

        private void OnClickButton()
        {
            if (!_bIsSpinning && !_bIsStopping)
            {
                StartInfiniteSpin();
            }
            else if (_bIsSpinning && !_bIsStopping)
            {
                StartNaturalStop();
            }
        }

        private void StartInfiniteSpin()
        {
            _bIsSpinning = true;
            _bIsStopping = false;

            _button.Text.SetText("STOP");

            ContinuousSpinLoop();
        }

        private void ContinuousSpinLoop()
        {
            if (_bIsStopping) return;

            float currentY = _contentRect.anchoredPosition.y;
            float targetY = currentY + _normalSpinSpeed;

            // 무한 스크롤 리셋
            if (targetY > _contentRect.sizeDelta.y * 0.8f)
            {
                ResetScrollPosition();
                targetY = _contentRect.anchoredPosition.y + _normalSpinSpeed;
            }

            _spinTween?.Kill();
            _spinTween = _contentRect.DOAnchorPosY(targetY, 1f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (_bIsSpinning && !_bIsStopping)
                    {
                        ContinuousSpinLoop();
                    }
                });
        }

        private void StartNaturalStop()
        {
            _bIsStopping = true;
            _button.Text.SetText("STOPPING...");

            _spinTween?.Kill();

            // 간단한 방법: 현재 위치에서 몇 칸 더 가서 멈추기
            NaturalStopAnimation();
        }

        private void NaturalStopAnimation()
        {
            float currentY = _contentRect.anchoredPosition.y;

            // 슬롯 하나의 높이 (간격 포함)
            float slotHeight = _itemHeight + _spacing;

            // 현재 위치에서 몇 칸 더 이동할 거리 계산
            float additionalDistance = _stopDistance * slotHeight;

            // 목표 위치 = 현재 위치 + 추가 거리
            float roughTargetY = currentY + additionalDistance;

            // 가장 가까운 슬롯 위치로 정렬 (슬롯 중앙에 멈추도록)
            float alignedTargetY = AlignToNearestSlot(roughTargetY);

            // 부드럽게 정지
            _spinTween = _contentRect.DOAnchorPosY(alignedTargetY, _stopDuration)
                .SetEase(_stopEase)
                .OnUpdate(() =>
                {
                    // 정지 중 업데이트 (필요시 효과 추가)
                })
                .OnComplete(() => { OnSpinComplete(); });
        }

        private float AlignToNearestSlot(float targetY)
        {
            // 간단한 정렬: 슬롯 높이로 나누어 가장 가까운 슬롯 위치 찾기
            float slotHeight = _itemHeight + _spacing;

            // _topOffset을 고려한 정렬
            float adjustedY = targetY - _topOffset;
            float slotIndex = Mathf.Round(adjustedY / slotHeight);
            float alignedY = (slotIndex * slotHeight) + _topOffset;

            return alignedY;
        }

        private void OnSpinComplete()
        {
            _bIsSpinning = false;
            _bIsStopping = false;

            _button.Text.SetText("START");

            // 결과 처리
            ProcessSpinResult();
        }

        private void ProcessSpinResult()
        {
            // 현재 중앙에 있는 슬롯 찾기
            Slot centerSlot = FindCenterSlot();

            if (centerSlot != null)
                ShowResultEffect();
        }

        private Slot FindCenterSlot()
        {
            // 뷰포트 중앙에 가장 가까운 슬롯 찾기
            if (_scrollRect == null || _scrollRect.viewport == null) return null;

            Slot closestSlot = null;
            float minDistance = float.MaxValue;

            // 뷰포트의 중앙 Y 위치 (로컬 좌표)
            float viewportCenterY = _scrollRect.viewport.rect.height * 0.5f;

            foreach (var slot in _slotList)
            {
                if (!slot.gameObject.activeInHierarchy) continue;

                Slot slotComponent = slot as Slot;
                if (slotComponent == null) continue;

                // 슬롯의 월드 위치를 뷰포트 로컬 좌표로 변환
                Vector3 slotWorldPos = slotComponent.RectTransform.TransformPoint(Vector3.zero);
                Vector3 slotViewportPos = _scrollRect.viewport.InverseTransformPoint(slotWorldPos);

                // 뷰포트 중앙과의 Y축 거리
                float distance = Mathf.Abs(slotViewportPos.y);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestSlot = slotComponent;
                }
            }

            return closestSlot;
        }

        private void ShowResultEffect()
        {
            // 결과 애니메이션
            _button.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 10, 1);
            _scrollRect.transform.DOPunchScale(Vector3.one * .1f, .3f, 10, 1);
        }

        private void ResetScrollPosition()
        {
            float resetY = _contentRect.sizeDelta.y * 0.4f;
            _contentRect.anchoredPosition = new Vector2(
                _contentRect.anchoredPosition.x,
                resetY
            );
        }

        // 디버그 및 설정 함수들

        [Button("수동 정지 테스트")]
        private void TestManualStop()
        {
            if (_bIsSpinning && !_bIsStopping)
            {
                StartNaturalStop();
            }
        }
    }
}