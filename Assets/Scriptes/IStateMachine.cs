using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public bool IsFree { get; }
    public IInteractive Storage { get; }
    public AnimationController AnimationController { get; }

    public void PutInStorage(IInteractive storage);
    public void TransitionState(State key);
}