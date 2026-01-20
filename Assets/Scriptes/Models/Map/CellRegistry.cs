using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CellRegistry : MonoBehaviour
{
    [SerializeField] private Map _map;

    private HashSet<Cell> _freeCells = new HashSet<Cell>();
    private HashSet<Cell> _occupiedCells = new HashSet<Cell>();

    private GridCreator _gridCreator;

    public IReadOnlyList<Cell> OccupiedCells => _occupiedCells.ToList();

    private void OnEnable()
    {
        if (_gridCreator == null)
            return;

        foreach (Cell cell in _freeCells)
            cell.Freed += ReleaseCell;
    }

    private void OnDisable()
    {
        if (_gridCreator == null)
            return;

        foreach (Cell cell in _freeCells)
            cell.Freed -= ReleaseCell;
    }

    public void Initialize()
    {
        _gridCreator = new GridCreator();
        _gridCreator.Create(_map);

        _freeCells = new HashSet<Cell>(_gridCreator.AllCells);

        gameObject.SetActive(true);
    }

    public void OccupyCell(Mineral mineral)
    {
        int index = Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells.ElementAt(index);

        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);
        
        cell.OccupyWithItem(mineral);
    }

    public void ReleaseCell(Cell cell)
    {
        _occupiedCells.Remove(cell);
        _freeCells.Add(cell);
    }
}
