using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Runtime.CompilerServices;
using System.Collections;

public class GradageSpawner : MonoBehaviour
{
    private const int BasePlaneScale = 10;

    [SerializeField] private Gardage _prefab;
    [SerializeField] private Map _map;

    [SerializeField, Range(0, 5)] private int _count;
    [SerializeField] private float _distance;

    private List<Gardage> _gardages = new List<Gardage>();

    private Timer _timer;
    private Coroutine _corutine;

    private float _halfScaleMapX;
    private float _halfScaleMapZ;

    private void Awake()
    {
        _timer = new Timer();

        _halfScaleMapX = _map.transform.localScale.x * BasePlaneScale / 2;
        _halfScaleMapZ = _map.transform.localScale.z * BasePlaneScale / 2;
    }

    private void Start()
    {
        _corutine = StartCoroutine(Spawn());
    }

    private void Update()
    {
        // Проверка прошло ли время у таймера, если да запускаю карутину снова
    }

    private bool CanSpawn(Vector3 targetPositon)
    {
        bool isInMap =  NavMesh.SamplePosition(targetPositon, out NavMeshHit navMeshHit, 1f, NavMesh.AllAreas);

        if (_gardages.Count == 0)
            return isInMap;

        bool isDistance = true;

        foreach (Gardage gardage in _gardages)
        {
            isDistance = (targetPositon - gardage.transform.position).sqrMagnitude >= _distance * _distance;

            if (isDistance == false)
                break;
        }

        return isInMap && isDistance;
    }

    private Vector3 GetRandomGradagePosition()
    {
        float positionX = Random.Range(_map.transform.position.x - _halfScaleMapX, _map.transform.position.x + _halfScaleMapX);
        float positionZ = Random.Range(_map.transform.position.z - _halfScaleMapZ, _map.transform.position.z + _halfScaleMapZ);

        return new Vector3(positionX, 0, positionZ);
    }

    private IEnumerator Spawn()
    {
        while (_gardages.Count < _count)
        {
            Vector3 position = GetRandomGradagePosition();

            if (CanSpawn(position))
            {
                Gardage gardage = Instantiate(_prefab, position, Quaternion.identity);

                _gardages.Add(gardage);
            }

            yield return null;
        }
    }
}
