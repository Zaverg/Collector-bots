using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public bool IsFree { get; }
    public ICollectable Storage { get; }
    public CollectorBotTask CurrentTask { get; }
    public AnimationController AnimationController { get; }

    public void SetStoredItem(ICollectable storage);
    public void DropItem();
}