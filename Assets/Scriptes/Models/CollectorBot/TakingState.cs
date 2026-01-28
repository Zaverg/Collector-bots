using System.Diagnostics;

public class TakingState : CollectorState
{
    private IStateMachine _stateMachine;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;

        if (_stateMachine is IResourceDeliverer resourceDeliverer)
        {
            ICollectable mineral = _stateMachine.CurrentTask.Mineral;
     
            resourceDeliverer.PlaceResourceInStorage(mineral);
            mineral.Take();
        }
    }

    public override void Run() { }

    public override void Exit() 
    {
        _stateMachine = null;
    }

    public override bool IsComplete()
    {
        return true;
    }
}