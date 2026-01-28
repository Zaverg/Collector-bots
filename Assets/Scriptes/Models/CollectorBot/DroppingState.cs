public class DroppingState : CollectorState
{
    private IStateMachine _stateMachine;

    public override void Entry(IStateMachine stateMachine) 
    {
        _stateMachine = stateMachine;

        if (_stateMachine is IResourceDeliverer resourceDeliverer)
        {
            ICollectable collectable = resourceDeliverer.ReleaseResource();
            collectable.Drope();
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
