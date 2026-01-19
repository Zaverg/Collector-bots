using UnityEngine;

public interface ICollectable
{
    public Transform Transform { get; }
    public MineralConfig Config { get; }

    public void OnPickedUp();
    public void OnDropped();
}