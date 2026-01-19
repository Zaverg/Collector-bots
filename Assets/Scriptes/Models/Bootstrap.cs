using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private ObjectPoolMineral _objectPullMineral;
    [SerializeField] private CoroutineStarter _coroutineStarter;
    [SerializeField] private CellRegistry _gridTracker;
    [SerializeField] private Map _map;

    private void Awake()
    {
        _gridTracker.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);

        _coroutineStarter.Initialize();
        _map.Initialize();
        _objectPullMineral.Initialize();
        _gridTracker.Initialize();
        _mineralSpawner.Initialize();
        _base.Initialize();        
    }
}
