using UnityEngine;

public class CollectorBotMining : Mining
{
    [SerializeField] private CoroutineRuner _runer;
    private Timer _timer;

    public override bool IsComplete => _timer.IsComplete;

    public void Awake()
    {
        _timer = new Timer(_runer);
    }

    public override void SetDiration(float duration)
    {
        _timer.SetDuration(duration);
    }

    public override void StartMining()
    {
        _timer.Run();
    }
}
