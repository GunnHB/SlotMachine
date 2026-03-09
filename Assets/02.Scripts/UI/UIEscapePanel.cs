using System;
using UnityEngine;

namespace _02.Scripts.UI
{
    public class UIEscapePanel : MonoBehaviour
    {
        [SerializeField] private GameObject popup = null;
        [SerializeField] private UIButton cancelButton = null;
        [SerializeField] private UIButton exitButton = null;

        public Action OnRequestClosePanel;

        public GameObject Popup => popup;

        private void Start()
        {
            BindCancelEvent();
            BindExitEvent();
        }

        private void BindCancelEvent()
        {
            if (cancelButton == null)
                return;
            
            cancelButton.onClick.AddListener(() =>
            {
                OnRequestClosePanel?.Invoke();
            });
        }

        private void BindExitEvent()
        {
            if (exitButton == null)
                return;
            
            exitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
        }
    }
}
