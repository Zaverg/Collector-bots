public abstract class CollectorState
{
    public abstract void Entry(IStateMachine stateMachine);
    public abstract void Run();
    public abstract void Exit();

    public abstract bool IsComplete();
}