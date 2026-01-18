using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private ObjectPullMineral _objectPullMineral;
    [SerializeField] private CoroutineStarter _coroutineStarter;
    [SerializeField] private GridTracker _gridTracker;
    [SerializeField] private Map _map;

    private void Awake()
    {
        _gridTracker.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);

        _coroutineStarter.Initialization();
        _map.Initialization();
        _objectPullMineral.Initialization();
        _gridTracker.Initialization();
        _mineralSpawner.Initialization();
        _base.Initialization();        
    }
}
