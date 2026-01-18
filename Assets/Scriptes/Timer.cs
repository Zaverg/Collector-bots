using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _targetSeconds;
    private float _currentSeconds;

    private bool _isEnd = false;
    private Coroutine _coroutine;
    
    public event Action Ended;
    public event Action<float> Changed;

    public bool IsEnd => _isEnd;

    public void SetSecond(float seconds) 
    {
        if (seconds < 0)
            return;

        _targetSeconds = seconds;
    }

    public void Run()
    {
        if (_targetSeconds == 0)
            return;

        _currentSeconds = _targetSeconds;
        _isEnd = false;

        _coroutine = CoroutineStarter.Instance.StartChildCoroutine(StartTimer());
    }

    public void Stop()
    {
        CoroutineStarter.Instance.StopChildCoroutine(_coroutine);
    }

    private IEnumerator StartTimer()
    {
        while(_currentSeconds > 0)
        {
            _currentSeconds -= Time.deltaTime;

            Changed?.Invoke(_currentSeconds);

            yield return null;
        }

        _isEnd = true;

        if (Ended != null)
            Ended.Invoke();
    }
}