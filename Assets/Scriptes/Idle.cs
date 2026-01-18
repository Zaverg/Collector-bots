public class Idle : State
{
    private IStateMachine _stateMachine;

    public override void Entry(IStateMachine stateMachine) 
    { 
        _stateMachine = stateMachine;
    }

    public override void Run()  { }

    public override void Exit() 
    {
        _stateMachine = null;
    }

    public override bool TryTransitionState()
    {
        if (_stateMachine.IsFree)
            return false;

        return true;
    }
}