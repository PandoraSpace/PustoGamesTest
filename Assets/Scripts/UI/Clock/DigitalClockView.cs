using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Clock
{
    public class DigitalClockView : MonoBehaviour
    {
        [SerializeField] private Text _timeText;

        private ClockViewModel _viewModel;

        public void Init(ClockViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.Date.OnChangeEvent += OnChangeTime;
        }

        private void OnChangeTime(DateTimeOffset date)
        {
            _timeText.text = string.Format("{0 : 00}:{1 : 00}:{2 : 00}", 
                date.Hour.ToString(), date.Minute.ToString(), date.Second.ToString());
        }

        private void OnDestroy()
        {
            _viewModel.Date.OnChangeEvent -= OnChangeTime;
        }
    }
}