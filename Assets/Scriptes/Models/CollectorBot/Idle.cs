public class Idle : CollectorState
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

    public override bool IsComplete()
    {
        if (_stateMachine.IsFree)
            return false;

        return true;
    }
}