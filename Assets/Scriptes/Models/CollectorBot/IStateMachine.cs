using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public bool HasTask { get; }
    public CollectorBotTask CurrentTask { get; }

    public IMoveble Mover { get; }
    public ITaker Taker { get; }
    public IMining Mining { get; }

    public UnitAnimator AnimationController { get; }
}