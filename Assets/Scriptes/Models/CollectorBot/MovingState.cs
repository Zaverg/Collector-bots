public class MovingState : CollectorState
{
    private IStateMachine _stateMachine;

    public override void Entry(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _stateMachine.Mover.SetTarget(stateMachine.CurrentTask.TargetPosition);
        _stateMachine.AnimationController.SetMoveAnimation(true);
    }

    public override void Run()
    {
        _stateMachine.Mover.Move();
    }

    public override void Exit()
    {
        _stateMachine.AnimationController.SetMoveAnimation(false);
        _stateMachine = null;
    }

    public override bool IsComplete()
    {
        return _stateMachine.Mover.IsPlace();
    }
}