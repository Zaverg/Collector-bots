using System;
using UnityEngine;

public class Cell
{
    private Vector3 _worldPosition;
    private Vector2Int _gridPosition;
    private bool _isFree = true;
    private IInteractive _item;

    public event Action<Cell> Freed;

    public Cell(Vector3 position, Vector2Int gridPosition)
    {
        _worldPosition = position;
        _gridPosition = gridPosition;
    }

    public Vector3 WorldPosition => _worldPosition;
    public Vector2Int GrisPosition => _gridPosition;
    public bool IsFree => _isFree;
    public IInteractive Item => _item;
 
    public void Fill(IInteractive item)
    {
        if (item == null)
            return;

        _isFree = false;
        _item = item;
    }

    public IInteractive TakeItem()
    {  
        _isFree = true;
        _item.Take();

        Freed?.Invoke(this);

        return _item;
    }
}