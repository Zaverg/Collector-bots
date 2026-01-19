using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _duration;
    private float _currentSeconds;

    private bool _isComplete = false;
    private Coroutine _coroutine;
    
    public event Action Ended;
    public event Action<float> Changed;

    public bool IsComplete => _isComplete;

    public void SetDuration(float duration) 
    {
        if (duration <= 0)
            return;

        _duration = duration;
    }

    public void Run()
    {
        if (_duration == 0)
            return;

        if (_coroutine != null)
            CoroutineStarter.Instance.StopChildCoroutine(_coroutine);

        _currentSeconds = _duration;
        _isComplete = false;

        _coroutine = CoroutineStarter.Instance.StartChildCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        float lastUpdateTime = _currentSeconds;
        float intervalUpdateUI = 0.9f;

        while(_currentSeconds > 0)
        {
            _currentSeconds -= Time.deltaTime;

            if (lastUpdateTime - _currentSeconds >= intervalUpdateUI)
            {
                Changed?.Invoke(_currentSeconds);
                lastUpdateTime = _currentSeconds;
            }

            yield return null;
        }

        _isComplete = true;

        if (Ended != null)
            Ended.Invoke();
    }
}