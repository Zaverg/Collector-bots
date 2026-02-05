using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CoroutineRuner _coroutineStarter;

    [Header("MineralSpawner")]
    [SerializeField] private SpawnGrid _cellRegister;
    [SerializeField] private ObjectPoolMineral _objectPullMineral;
    [SerializeField] private Map _map;

    [Header("Base")]
    [SerializeField] private DeliveryZone _deliverZone;
    [SerializeField] private ResurceCounter _reesurceCounter;
    [SerializeField] private MineralRegistry _mineralRegistry;
    [SerializeField] private CollectorBotDispatcher _collectorBotDispatcher;

    [Header("View")]
    [SerializeField] private MineralCountViewer _mineralCountView;

    private void Awake()
    {
        _cellRegister.gameObject.SetActive(false);
        _mineralSpawner.gameObject.SetActive(false);
        _base.gameObject.SetActive(false);

        _map.Initialize();
        _objectPullMineral.Initialize();
        _cellRegister.Initialize();

        _mineralSpawner.Initialize(_coroutineStarter, _mineralRegistry);

        _collectorBotDispatcher.Initialize();
        _base.Initialize(_coroutineStarter, _mineralRegistry);        
    }

    private void OnEnable()
    {
        _deliverZone.ResourceDelivered += HandleResourceDelivered;
        _reesurceCounter.MineralCountChanged += _mineralCountView.UpdateView;
    }

    private void OnDisable()
    {
        _deliverZone.ResourceDelivered -= HandleResourceDelivered;
        _reesurceCounter.MineralCountChanged -= _mineralCountView.UpdateView;
    }

    private void HandleResourceDelivered(ICollectable collectable)
    {
        _reesurceCounter.UpdateCounter(collectable);
        _mineralRegistry.RemoveMineral(collectable);
    }
}
