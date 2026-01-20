using System;
using UnityEngine;

public class Mineral : MonoBehaviour, IReleasable<Mineral>, ICollectable
{
    [SerializeField] private MineralStatus _status;

    [SerializeField] private MineralConfig _mineralConfig;

    public event Action<Mineral> Released;

    public MineralStatus Status => _status;
    public Transform Transform => transform;
    public MineralConfig Config => _mineralConfig;

    public void SetConfig(MineralConfig config)
    {
        if (config == null)
            return;

        _mineralConfig = config;

        GetComponent<MeshFilter>().mesh = _mineralConfig.Mesh;
        GetComponent<MeshRenderer>().material = _mineralConfig.Material;
    }

    public void OnPickedUp()
    {
        _status = MineralStatus.Taken;
    }

    public void OnDropped()
    {
        _status = MineralStatus.Dropped;
    }

    public void MarkAsScanned()
    {
        _status = MineralStatus.Scanned;
    }

    public void MarkAsTargeted()
    {
        _status = MineralStatus.Targeted;
    }

    public void ResetToPool()
    {
        _status = MineralStatus.Free;
        Released?.Invoke(this);
    }
}
