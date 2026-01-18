using UnityEngine;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Collector> _collectors = new List<Collector>();

    [SerializeField] private GridTracker _gridTracker;
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private InformationViewer _timerView;
    [SerializeField] private InformationViewer _countMineralView;

    [SerializeField] private float _scannedInterval;

    private Queue<Cell> _freeTargets = new Queue<Cell>();
    private Queue<Collector> _collectorsQueue;

    private Scanner _scanner;
    private Timer _timer;

    private void OnEnable()
    {
        if (_timer == null)
            return;
        
        _timer.Ended += ActivateScanner;
        _timer.Changed += _timerView.UpdateView;
        _dropZone.Changed += _countMineralView.UpdateView;

        foreach (Collector collector in _collectors)
            collector.Freed += PutInQueue;
    }

    private void OnDisable()
    {
        if (_timer == null)
            return;

        _timer.Ended -= ActivateScanner;
        _timer.Changed -= _timerView.UpdateView;
        _dropZone.Changed -= _countMineralView.UpdateView;

        foreach (Collector collector in _collectors)
            collector.Freed -= PutInQueue;
    }

    private void Start()
    {
        _timer.Run();
    }

    private void Update()
    {
        if (_freeTargets.Count == 0 || _collectorsQueue.Count == 0)
            return;

        Collector collector = _collectorsQueue.Dequeue();

        SetTargetToCollector(collector);
    }

    public void Initialization()
    {
        _scanner = new Scanner(_gridTracker);
        _timer = new Timer();

        _timer.SetSecond(_scannedInterval);
        _collectorsQueue = new Queue<Collector>(_collectors);

        gameObject.SetActive(true);
    }

    public void SetTargetToCollector(Collector collector)
    {
        Cell cell = _freeTargets.Dequeue();

        State[] states = new State[4] { new Mover(cell.WorldPosition), new Taker(cell), new Mover(_dropZone.transform.position), new Drop() };
        Queue<State> tasks = new Queue<State>(states);

        collector.SetTask(tasks);
    }

    private void ActivateScanner()
    {
        List<Cell> cells = _scanner.Scan();

        foreach (Cell cell in cells)
        {
            _freeTargets.Enqueue(cell);
        }

        _timer.Run();
    }

    private void PutInQueue(Collector collector)
    {
        _collectorsQueue.Enqueue(collector);
    }
}