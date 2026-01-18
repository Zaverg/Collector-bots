using UnityEngine;

public interface IInteractive
{
    public Transform Transform { get; }

    public void Take();
    public void Drop();
}