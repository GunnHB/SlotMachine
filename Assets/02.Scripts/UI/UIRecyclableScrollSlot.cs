using UnityEngine;

public abstract class UIRecyclableScrollSlot<T> : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTransform;
    public RectTransform RectTransform => _rectTransform;
    public float Height => _rectTransform.rect.height;
    public float Width => _rectTransform.rect.width;

    public abstract void Init();
    public abstract void UpdateSlot(T data);
}