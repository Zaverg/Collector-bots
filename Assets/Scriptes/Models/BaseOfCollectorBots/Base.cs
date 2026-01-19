using UnityEngine;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private List<CollectorBot> _collectors = new List<CollectorBot>();

    [SerializeField] private CellRegistry _gridTracker;
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private TimerViewer _scanIntervalView;
    [SerializeField] private MineralCountViewer _mineralCountView;

    [SerializeField] private float _scanInterval;

    private Queue<Cell> _availableTargets = new Queue<Cell>();
    private Queue<CollectorBot> _availableCollectors;

    private Scanner _scanner;
    private Timer _timer;

    private void OnEnable()
    {
        if (_timer == null)
            return;
        
        _timer.Ended += ActivateScanner;
        _timer.Changed += _scanIntervalView.UpdateView;
        _dropZone.MineralCountChanged += _mineralCountView.UpdateView;

        foreach (CollectorBot collector in _collectors)
            collector.Freed += EnqueueCollector;
    }

    private void OnDisable()
    {
        if (_timer == null)
            return;

        _timer.Ended -= ActivateScanner;
        _timer.Changed -= _scanIntervalView.UpdateView;
        _dropZone.MineralCountChanged -= _mineralCountView.UpdateView;

        foreach (CollectorBot collector in _collectors)
            collector.Freed -= EnqueueCollector;
    }

    private void Start()
    {
        _timer.Run();
    }

    private void Update()
    {
        if (_availableTargets.Count == 0 || _availableCollectors.Count == 0)
            return;

        CollectorBot collector = _availableCollectors.Dequeue();

        AssignMiningTask(collector);
    }

    public void Initialize()
    {
        _scanner = new Scanner(_gridTracker);
        _timer = new Timer();

        _timer.SetDuration(_scanInterval);
        _availableCollectors = new Queue<CollectorBot>(_collectors);

        gameObject.SetActive(true);
    }

    public void AssignMiningTask(CollectorBot collector)
    {
        Cell cell = _availableTargets.Dequeue();

        Queue<CollectorBotTask> tasks = new Queue<CollectorBotTask>();

        tasks.Enqueue(new CollectorBotTask(StateType.Moving, cell.WorldPosition));
        tasks.Enqueue(new CollectorBotTask(StateType.Mining, cell: cell));
        tasks.Enqueue(new CollectorBotTask(StateType.Taking, cell: cell));
        tasks.Enqueue(new CollectorBotTask(StateType.Moving, _dropZone.transform.position));
        tasks.Enqueue(new CollectorBotTask(StateType.Dropping));

        collector.AssignTask(tasks);
    }

    private void ActivateScanner()
    {
        List<Cell> cells = _scanner.ScanForFreeMinerals();

        foreach (Cell cell in cells)
        {
            _availableTargets.Enqueue(cell);
        }

        _timer.Run();
    }

    private void EnqueueCollector(CollectorBot collector)
    {
        _availableCollectors.Enqueue(collector);
    }
}