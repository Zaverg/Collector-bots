using System;
using UnityEngine;

public class ResurceCounter : MonoBehaviour
{
    private int _collectedResources;

    public event Action<int> MineralCountChanged;

    public void UpdateCounter(ICollectable collectable)
    {
        if (collectable is Mineral)
        {
            _collectedResources++;
            MineralCountChanged?.Invoke(_collectedResources);
        }
    }
}