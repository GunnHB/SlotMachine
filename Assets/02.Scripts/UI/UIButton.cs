using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace _02.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class UIButton : Button
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        public TextMeshProUGUI Text => _text;

        protected override void Awake()
        {
            if (_text == null)
                _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
