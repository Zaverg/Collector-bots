using System;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    private int _collectedMinerals;

    public event Action<int> MineralCountChanged;

    public void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Mineral mineral))
        {
            if (mineral.Status == MineralStatus.Dropped)
            {
                mineral.ResetToPool();
                _collectedMinerals++;
                MineralCountChanged?.Invoke(_collectedMinerals);
            }
        }
    }
}