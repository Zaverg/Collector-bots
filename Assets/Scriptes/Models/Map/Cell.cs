using System;
using UnityEngine;

public class Cell
{
    private Vector3 _worldPosition;
    private Vector2Int _gridPosition;
    private ICollectable _item;

    public event Action<Cell> Freed;

    public Cell(Vector3 position, Vector2Int gridPosition)
    {
        _worldPosition = position;
        _gridPosition = gridPosition;
    }

    public Vector3 WorldPosition => _worldPosition;
    public Vector2Int GridPosition => _gridPosition;
    public bool IsFree => _item == null;
    public ICollectable Item => _item;
 
    public void OccupyWithItem(ICollectable item)
    {
        _item = item;
    }

    public ICollectable ExtractItem()
    {
        ICollectable item = _item;
        Freed?.Invoke(this);
        _item = null;

        return item;
    }
}