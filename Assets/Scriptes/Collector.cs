using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(AnimationController))]
public class Collector : MonoBehaviour, IStateMachine
{
    [SerializeField] private bool _isFree;

    private Queue<State> _tasks = new Queue<State>();

    private AnimationController _animationController;

    private State _current;
    private State _idle;

    private Vector3 _targetPosition;
    private Transform _deliveryPosition;

    private IInteractive _storage;

    private bool _isTransitioning = false;

    public event Action<Collector> Freed;

    public bool IsFree => _tasks.Count <= 0;
    public bool IsTransitioning => _isTransitioning;

    public Transform DeliveryPosition => _deliveryPosition;
    public Transform Transform => transform;
    public IInteractive Storage => _storage;
    public AnimationController AnimationController => _animationController;

    public void Awake()
    {
        _idle = new Idle();
        _current = _idle;

        _animationController = GetComponent<AnimationController>();

        _current.Entry(this);
    }

    private void Update()
    {
        _isFree = _tasks.Count <= 0;

        bool isCompleted = _current.TryTransitionState();

        if (isCompleted == false)
        {
            _current.Run();
        }
        else
        {
            if (_tasks.Count > 0)
                TransitionState(_tasks.Dequeue());
            else
                TransitionState(_idle);
        }
    }

    public void SetTask(Queue<State> tasks)
    {
        _tasks = new Queue<State>(tasks);
    }

    public void PutInStorage(IInteractive item)
    {
        _storage = item;
    }

    public void TransitionState(State state)
    {
        _isTransitioning = true;

        _current.Exit();

        _current = state;
        _current.Entry(this);

        _isTransitioning = false;

        if (_current == _idle)
            Freed?.Invoke(this);
    }
}
