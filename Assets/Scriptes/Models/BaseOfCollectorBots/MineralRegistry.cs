using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineralRegistry : MonoBehaviour
{
    private HashSet<ICollectable> _occupiedMinerals = new HashSet<ICollectable>();
    private HashSet<ICollectable> _availableMinerals = new HashSet<ICollectable>();

    public int AvailableMineralsCount => _availableMinerals.Count;

    public void Register(ICollectable collectable)
    {
        if (_occupiedMinerals.Contains(collectable) == false)
        {
            if (collectable.Transform.gameObject.activeSelf)
                _availableMinerals.Add(collectable);
        }
    }

    public ICollectable GetAvailableMineral()
    {
        ICollectable collectable = _availableMinerals.ElementAt(0);

        _availableMinerals.Remove(collectable);
        _occupiedMinerals.Add(collectable);

        collectable.Dropped += OnMineralDropped;

        return collectable;
    }

    public void RemoveMineral(ICollectable collectable)
    {
        collectable.Dropped -= OnMineralDropped;
        collectable.ReturnToPool();

        _occupiedMinerals.Remove(collectable);
    }

    private void OnMineralDropped(ICollectable collectable)
    {
        collectable.Dropped -= OnMineralDropped;
        _occupiedMinerals.Remove(collectable);
    }
}