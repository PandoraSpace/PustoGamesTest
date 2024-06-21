using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Clock
{
    public class ClockView : MonoBehaviour
    {
        [SerializeField] private ClockHandView _hourHand;
        [SerializeField] private ClockHandView _minuteHand;
        [SerializeField] private ClockHandView _secondHand;
        [SerializeField] private Toggle _editTogle;
        [Range(0.1f, 0.5f)] [SerializeField] private float _clockHandSpeed;
        
        private const float HOUR_DEGREES = 360f / 12f;
        private const float MINUTE_DEGREES = 360f / 60f;
        private const float SECOND_DEGREES = 360f / 60f;

        private ClockViewModel _viewModel;

        public void Init(ClockViewModel viewModel)
        {
            _viewModel = viewModel;
            
            _editTogle.onValueChanged.AddListener(_viewModel.OnEditChange);
            _viewModel.Date.OnChangeEvent += OnChangeTime;
            _hourHand.OnDragEvent += _viewModel.OnDragHourHandView;
            _minuteHand.OnDragEvent += _viewModel.OnDragMinuteHandView;
            _secondHand.OnDragEvent += _viewModel.OnDragSecondHandView;
        }

        public void DisplayTime(DateTimeOffset date)
        {
            _viewModel.OnChangeTimeView(date);
        }

        public void DisplayTime()
        {
            _viewModel.OnChangeTimeView();
        }

        private void OnChangeTime(DateTimeOffset date)
        {
            _hourHand.Transform.DORotate(new Vector3(0f, 0f, -date.Hour * HOUR_DEGREES), _clockHandSpeed);
            _minuteHand.Transform.DORotate(new Vector3(0f, 0f, -date.Minute * MINUTE_DEGREES), _clockHandSpeed);
            _secondHand.Transform.DORotate(new Vector3(0f, 0f, -date.Second * SECOND_DEGREES), _clockHandSpeed);
        }

        private void OnDestroy()
        {
            _editTogle.onValueChanged.RemoveListener(_viewModel.OnEditChange);
            _viewModel.Date.OnChangeEvent -= OnChangeTime;
            _hourHand.OnDragEvent -= _viewModel.OnDragHourHandView;
            _minuteHand.OnDragEvent -= _viewModel.OnDragMinuteHandView;
            _secondHand.OnDragEvent -= _viewModel.OnDragSecondHandView;
            
            _viewModel.Dispose();
        }
    }
}