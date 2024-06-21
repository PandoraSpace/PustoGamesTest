using System;
using Infrastructure;
using UnityEngine;

namespace UI.Clock
{
    public class ClockViewModel : IDisposable
    {
        private readonly ClockModel _model;

        private DateTimeOffset _currentDate;
        private bool _isEdit;
        private int _newHour;
        private int _newMinute;
        private int _newSecond;
        
        private const float HOUR_DEGREES = 360f / 12f;
        private const float MINUTE_DEGREES = 360f / 60f;
        private const float SECOND_DEGREES = 360f / 60f;
        
        public ReactiveProperty<DateTimeOffset> Date = new ReactiveProperty<DateTimeOffset>(DateTimeOffset.Now);

        public ClockViewModel(ClockModel model)
        {
            _model = model;
            
            _model.Date.OnChangeEvent += OnChangeModel;
        }

        public void OnChangeTimeView(DateTimeOffset date)
        {
            _model.Date.Value = date;
            _currentDate = date;
        }

        public void OnChangeTimeView()
        {
            if (_isEdit == false)
                OnChangeTimeView(_currentDate.AddSeconds(1));
        }

        public void OnDragHourHandView(Vector2 pos, Transform t)
        {
            if (!_isEdit) return;
            
            MoveHand(pos, t);
            _newHour = Mathf.RoundToInt(Mathf.Abs(360f - t.rotation.eulerAngles.z) / HOUR_DEGREES);
        }
        
        public void OnDragMinuteHandView(Vector2 pos, Transform t)
        {
            if (!_isEdit) return;
            
            MoveHand(pos, t);
            _newMinute = Mathf.RoundToInt(Mathf.Abs(360f - t.rotation.eulerAngles.z) / MINUTE_DEGREES);
        }
        
        public void OnDragSecondHandView(Vector2 pos, Transform t)
        {
            if (!_isEdit) return;
            
            MoveHand(pos, t);
            _newSecond = Mathf.RoundToInt(Mathf.Abs(360f - t.rotation.eulerAngles.z) / SECOND_DEGREES);
        }

        public void OnEditChange(bool isEdit)
        {
            _isEdit = isEdit;

            if (isEdit)
            {
                _newHour = 0;
                _newMinute = 0;
                _newSecond = 0;
                
                return;
            }

            if (_newHour != 0) OnChangeTimeView(_currentDate.AddHours(_newHour - _currentDate.Hour));
            if (_newMinute != 0) OnChangeTimeView(_currentDate.AddMinutes(_newMinute - _currentDate.Minute));
            if (_newSecond != 0) OnChangeTimeView(_currentDate.AddSeconds(_newSecond - _currentDate.Second));
        }

        private void MoveHand(Vector2 pos, Transform t)
        {
            var diff = pos - (Vector2) t.position;
            diff.Normalize();
            var angleZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            t.rotation = Quaternion.Euler(0f, 0f, angleZ - 90);
        }

        private void OnChangeModel(DateTimeOffset date)
        {
            Date.Value = date;
        }

        public void Dispose()
        {
            _model.Date.OnChangeEvent -= OnChangeModel;
        }
    }
}