using UnityEngine;

public class TakingState : CollectorState
{
    private IStateMachine _stateMachine;
    private Cell _cell;

    private bool _isTake = false;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;
        _cell = _stateMachine.CurrentTask.Cell;

        ICollectable interactive = _cell.ExtractItem();
        interactive.OnPickedUp();
        _stateMachine.SetStoredItem(interactive);

        _isTake = true;
    }

    public override void Run() { }

    public override void Exit() 
    {
        _stateMachine = null;
        _isTake = false;
        _cell = null;
    }

    public override bool IsComplete()
    {
        return _isTake;
    }
}