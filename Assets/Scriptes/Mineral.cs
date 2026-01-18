using System;
using UnityEngine;

public class Mineral : MonoBehaviour, IReleasable<Mineral>, IInteractive
{
    [SerializeField] private MineralStatus _status;

    [SerializeField] private MineralConfig _mineralConfig;

    public event Action<Mineral> Released;

    public MineralStatus Status => _status;
    public Transform Transform => transform;
    public MineralConfig Config => _mineralConfig;

    public Cell Cell;

    public bool IsFree;
    public Mineral Mineral1;
    public Vector3 Position;
    public Vector2Int GridPosition;

    private void OnEnable()
    {
        _status = MineralStatus.Free;
    }

    private void Start()
    {
        IsFree = Cell.IsFree;
        Position = Cell.WorldPosition;
        GridPosition = Cell.GrisPosition;
    }

    public void SetConfig(MineralConfig config)
    {
        if (config == null)
            return;

        if (config.GetType() != typeof(MineralConfig))
            return;

        _mineralConfig = config;

        GetComponent<MeshFilter>().mesh = _mineralConfig.Mesh;
        GetComponent<MeshRenderer>().material = _mineralConfig.Material;
    }

    public void Take()
    {
        _status = MineralStatus.Taken;
    }

    public void Drop()
    {
        transform.SetParent(null);
    }

    public void Found()
    {
        _status = MineralStatus.Scanned;
    }

    public void Mark()
    {
        _status = MineralStatus.Market;
    }

    public void OnReset()
    {
        Released?.Invoke(this);
    }
}
