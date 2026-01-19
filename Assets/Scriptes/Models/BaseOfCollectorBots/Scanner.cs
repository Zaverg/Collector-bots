using System.Collections.Generic;

public class Scanner
{
    private CellRegistry _gridTracker;

    public Scanner(CellRegistry gridTracker)
    {
        _gridTracker = gridTracker;
    }
   
    public List<Cell> ScanForFreeMinerals()
    {
        List<Cell> scannedMinerals = new List<Cell>();

        foreach (Cell cell in _gridTracker.OccupiedCells)
        {
            ICollectable interactive = cell.Item;

            if (interactive is Mineral mineral)
            {
                if (mineral.Status != MineralStatus.Free)
                    continue;

                mineral.MarkAsScanned();
                scannedMinerals.Add(cell);
            }
        }

        return scannedMinerals;
    }
}