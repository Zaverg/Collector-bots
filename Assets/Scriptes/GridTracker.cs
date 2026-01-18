using UnityEngine;
using System.Collections.Generic;

public class GridTracker : MonoBehaviour
{
    [SerializeField] private Map _map;
    private Cell[,] _grid;

     private List<Cell> _freeCells = new List<Cell>();
     private List<Cell> _occupiedCells = new List<Cell>();

    private GridCreator _gridCreator;

    public IReadOnlyList<Cell> OccupiedCells => _occupiedCells;

    private void OnEnable()
    {
        if (_gridCreator == null)
            return;

        foreach (Cell cell in _freeCells)
            cell.Freed += FreeCell;
    }

    private void OnDisable()
    {
        if (_gridCreator == null)
            return;

        foreach (Cell cell in _freeCells)
            cell.Freed -= FreeCell;
    }

    public void Initialization()
    {
        _gridCreator = new GridCreator();
        _grid = _gridCreator.Create(_map);

        _freeCells = _gridCreator.GetListCell();

        gameObject.SetActive(true);
    }

    public void FillCell(Mineral mineral)
    {
        int index = Random.Range(0, _freeCells.Count);

        Cell cell = _freeCells[index];

        _freeCells.Remove(cell);
        _occupiedCells.Add(cell);
        
        cell.Fill(mineral);
        mineral.Cell = cell;
        mineral.transform.position = new Vector3(cell.WorldPosition.x, cell.WorldPosition.y, cell.WorldPosition.z);
    }

    public void FreeCell(Cell cell)
    {
        _occupiedCells.Remove(cell);
        _freeCells.Add(cell);
    }
}
