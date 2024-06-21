using System.Collections;
using Infrastructure;
using UI.Clock;
using UnityEngine;

namespace Service
{
    public class ClockTime
    {
        private readonly ClockView _clockView;
        private readonly Coroutines _coroutines;
        private readonly WaitForSeconds _wait;

        public ClockTime(ClockView clockView, Coroutines coroutines)
        {
            _clockView = clockView;
            _coroutines = coroutines;
            
            _wait = new WaitForSeconds(1f);
        }

        public void Start()
        {
            _coroutines.StartCoroutine(ClockTimer());
        }

        private IEnumerator ClockTimer()
        {
            while (true)
            {
                yield return _wait;
                
                _clockView.DisplayTime();
            }
        }
    }
}