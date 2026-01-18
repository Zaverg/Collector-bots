public class Drop : State
{
    private IStateMachine _stateMachine;

    private bool _isDrop = false;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;
    }

    public override void Run()
    {
        if (_isDrop == false && _stateMachine.Storage != null)
        {
            _stateMachine.Storage.Drop();
            _isDrop = true;
        }
    }

    public override void Exit()
    {
        _stateMachine = null;
        _isDrop = false;
    }

    public override bool TryTransitionState()
    {
        return _isDrop;
    }
}
