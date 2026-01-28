using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public bool IsFree { get; }
    public CollectorBotTask CurrentTask { get; }
    public UnitAnimator AnimationController { get; }
}