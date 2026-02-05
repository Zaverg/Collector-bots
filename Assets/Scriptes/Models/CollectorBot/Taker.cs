using UnityEngine;

public abstract class Taker : MonoBehaviour, ITaker
{
    public abstract void PlaceResourceInStorage(ICollectable collectable);

    public abstract ICollectable ReleaseResource();

    protected abstract void ClearStorag();
}