public class MiningState : CollectorState
{
    private IStateMachine _stateMachine;
    private Timer _timer;
    private Cell _cell;

    public override void Entry(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _timer = new Timer();
        _cell = _stateMachine.CurrentTask.Cell;

        ICollectable collectable = _cell.Item;
        float duration = collectable.Config.MiningDuration;

        _timer.SetDuration(duration);
        _timer.Run();
    }

    public override void Run() { }

    public override void Exit()
    {
        _timer = null;
        _cell = null;
        _stateMachine = null;
    }

    public override bool IsComplete()
    {
        if (_timer.IsComplete)
            return true;

        return false;
    }
}
