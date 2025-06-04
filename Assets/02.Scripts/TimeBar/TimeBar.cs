using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using _02.Scripts.UI;
using UnityEngine.Serialization;

namespace _02.Scripts.TimeBar
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar = null;
        [SerializeField] private Image _progressBarImage = null;
        [SerializeField] private UIButton _button = null;
        [SerializeField] private float _initialTime = 0f;

        [SerializeField] private Color _startColor = Color.green;
        [SerializeField] private Color _midColor = Color.yellow;
        [SerializeField] private Color _endColor = Color.red;

        private Coroutine _timeCoroutine = null;

        private float _currTime = 0f;
        private float _midTime = 0f;
        private bool _bIsTimerRunning = false;

        private void Start()
        {
            if (_progressBar != null)
                _progressBar.value = 1f;

            if (_button != null)
            {
                _button.Text.SetText(_initialTime.ToString());
                _button.onClick.AddListener(OnClickButton);
            }

            if (_progressBarImage != null)
                _progressBarImage.color = _startColor;

            _currTime = _initialTime;
            _midTime = _initialTime / 2;
        }

        private void OnClickButton()
        {
            if (_bIsTimerRunning == false)
                StartTimer();
            else
                StopTimer();
        }

        private void StartTimer()
        {
            if(_timeCoroutine != null)
                StopCoroutine(_timeCoroutine);

            if (_currTime <= 0f)
                _currTime = _initialTime;

            _timeCoroutine = StartCoroutine(TimerCoroutine());
            _bIsTimerRunning = true;
        }

        private void StopTimer()
        {
            if (_timeCoroutine != null)
            {
                StopCoroutine(_timeCoroutine);
                _timeCoroutine = null;
            }

            _bIsTimerRunning = false;
        }

        private IEnumerator TimerCoroutine()
        {
            while (_currTime > 0f)
            {
                _currTime -= Time.deltaTime;

                int temp = Mathf.RoundToInt(_currTime);
                _button.Text.SetText(temp.ToString());
                
                UpdateProgressBar();
                
               yield return null;
            }

            _currTime = 0f;
            _progressBar.value = 0f;
            _progressBarImage.color = _endColor;

            _bIsTimerRunning = false;
            
            _timeCoroutine = null;
        }

        private void UpdateProgressBar()
        {
            _progressBar.value = _currTime / _initialTime;

            if (_currTime > _midTime)
            {
                float normalize = (_currTime - _midTime) / (_initialTime - _midTime);
                _progressBarImage.color = Color.Lerp(_midColor, _startColor, normalize);
            }
            else
            {
                float normalize = _currTime / _midTime;
                _progressBarImage.color = Color.Lerp(_endColor, _midColor, normalize);
            }
        }
    }
}
