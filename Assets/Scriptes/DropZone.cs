using System;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    private int _countMinerals;

    public event Action<int> Changed;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Mineral mineral))
        {
            _countMinerals++;
            mineral.OnReset();

            Changed?.Invoke(_countMinerals);
        }
    }
}