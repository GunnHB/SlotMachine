using UnityEngine;
using UnityEngine.UI;

namespace _02.Scripts.UI
{
    public class UISafeArea : MonoBehaviour
    {
        public RectTransform rectTransform; // safeArea 스크립트 붙는 컴포넌트 넣어주기
        private Rect _safeArea;
        private Vector2 _minAnchor;
        private Vector2 _maxAnchor;

        private void Awake()
        {
            // safeArea를 받아서 min 앵커와 max 앵커에 Position 부여
            // 픽셀로 반환되니 앵커에 넣기 위해서는 비율로 변환 필요
            _safeArea = Screen.safeArea;
            _minAnchor = _safeArea.position;
            _maxAnchor = _minAnchor + _safeArea.size;

            // 인스펙터 프로퍼티에 집어넣을 수 있게 비율로 변환 및 할당
            _minAnchor.x /= Screen.width;
            _minAnchor.y /= Screen.height;
            _maxAnchor.x /= Screen.width;
            _maxAnchor.y /= Screen.height;

            rectTransform.anchorMin = _minAnchor;
            rectTransform.anchorMax = _maxAnchor;
        }
    }
}
