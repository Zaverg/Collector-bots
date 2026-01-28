using System;
using UnityEngine;

public interface ICollectable
{
    public Transform Transform { get; }
    public MineralConfig Config { get; }

    public event Action<ICollectable> Taked;
    public event Action<ICollectable> Dropped;

    public void Take();
    public void Drope();
    public void ReturnToPool();
}