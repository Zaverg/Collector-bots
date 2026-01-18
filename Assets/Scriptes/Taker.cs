using UnityEngine;

public class Taker : State
{
    private IStateMachine _stateMachine;
    private Cell _cell;

    private bool _isTake = false;

    public Taker(Cell cell)
    {
        _cell = cell;
    }

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;
    }

    public override void Run()
    {
        if (_isTake == false)
        {
            IInteractive interactive = _cell.TakeItem();
            _stateMachine.PutInStorage(interactive);

            interactive.Transform.SetParent(_stateMachine.Transform);
            interactive.Transform.localPosition = Vector3.zero;

            _isTake = true;      
        }
    }

    public override void Exit() 
    {
        _stateMachine = null;
        _isTake = false;
    }

    public override bool TryTransitionState()
    {
        return _isTake;
    }
}