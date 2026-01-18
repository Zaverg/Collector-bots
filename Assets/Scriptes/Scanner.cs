using System.Collections.Generic;

public class Scanner
{
    private GridTracker _gridTracker;

    public Scanner(GridTracker gridTracker)
    {
        _gridTracker = gridTracker;
    }
   
    public List<Cell> Scan()
    {
        List<Cell> scannedMinerals = new List<Cell>();

        foreach (Cell cell in _gridTracker.OccupiedCells)
        {
            IInteractive interactive = cell.Item;

            if (interactive is Mineral mineral)
            {
                if (mineral.Status != MineralStatus.Free)
                    continue;

                mineral.Found();
                scannedMinerals.Add(cell);
            }
        }

        return scannedMinerals;
    }
}