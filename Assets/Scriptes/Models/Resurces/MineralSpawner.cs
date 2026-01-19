using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private CellRegistry _gridTracker;

    [SerializeField, Range(0, 5)] private int _count;

    [SerializeField] private ObjectPoolMineral _pull;

    [SerializeField] private float _intensity;
    [SerializeField] private TimerViewer _timerView;

    private Timer _timer;
    private int _currentCount;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        if (_timer == null)
            return;

        _timer.Ended += StartSpawn;
        _timer.Changed += _timerView.UpdateView;
    }

    private void OnDisable()
    {
        if (_timer == null)
            return;

        _timer.Ended -= StartSpawn;
        _timer.Changed -= _timerView.UpdateView;
    }

    private void Start()
    {
        StartSpawn();
    }

    public void Initialize()
    {
        _timer = new Timer();
        _timer.SetDuration(_intensity);

        gameObject.SetActive(true);
    }

    private void StartSpawn()
    {
        if (_coroutine != null)
            return;

        _currentCount = _gridTracker.OccupiedCells.Where(cell => cell.Item is Mineral).Count();
        _coroutine = StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        int maxIndex = Enum.GetValues(typeof(MineralType)).Length;

        while (_currentCount < _count)
        {
            int indexType = UnityEngine.Random.Range(0, maxIndex);
            MineralType type = (MineralType)indexType;

            Mineral mineral = _pull.GetMineral(type);
            _gridTracker.OccupyCell(mineral);
            
            _currentCount++;
            
            yield return null;
        }

        _coroutine = null;
        _timer.Run();
    }
}
