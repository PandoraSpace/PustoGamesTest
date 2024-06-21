using Config;
using Infrastructure;
using Service;
using UI.Clock;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ClockView _clockView;
    [SerializeField] private DigitalClockView _digitalClockView;
    [SerializeField] private Configs _configs;
    private CheckTime _checkTime;
    
    private void Start()
    {
        var coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        var clockTimer = new ClockTime(_clockView, coroutines);
        
        _checkTime = new CheckTime(coroutines, _configs.TimeRequest);
        _checkTime.OnRequestDate += _clockView.DisplayTime;
        
        InitClock();
        
        _checkTime.Start();
        clockTimer.Start();
    }

    private void InitClock()
    {
        var model = new ClockModel();
        var viewModel = new ClockViewModel(model);
        
        _clockView.Init(viewModel);
        _digitalClockView.Init(viewModel);
    }

    private void OnDestroy()
    {
        _checkTime.OnRequestDate -= _clockView.DisplayTime;
    }
}