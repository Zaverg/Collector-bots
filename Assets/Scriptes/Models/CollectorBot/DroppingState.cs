public class DroppingState : CollectorState
{
    private IStateMachine _stateMachine;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;

        if (_stateMachine.Storage != null)
            _stateMachine.DropItem();
    }

    public override void Run() { }

    public override void Exit()
    {
        _stateMachine = null;
    }

    public override bool IsComplete()
    {
        return _stateMachine.Storage == null;
    }
}
