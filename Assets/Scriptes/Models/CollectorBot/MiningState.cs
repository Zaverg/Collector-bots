public class MiningState : CollectorState
{
    private IStateMachine _stateMachine;

    public override void Entry(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        ICollectable collectable = _stateMachine.CurrentTask.Mineral;
        float duration = collectable.Config.MiningDuration;

        _stateMachine.Mining.SetDiration(duration);
        _stateMachine.Mining.StartMining();
    }

    public override void Run() { }

    public override void Exit()
    {
        _stateMachine = null;
    }

    public override bool IsComplete()
    {
        return _stateMachine.Mining.IsComplete;
    }
}
