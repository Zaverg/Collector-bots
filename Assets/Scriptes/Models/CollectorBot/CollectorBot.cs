using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(AnimationController))]
public class CollectorBot : MonoBehaviour, IStateMachine
{
    private Queue<CollectorBotTask> _tasks = new Queue<CollectorBotTask>();
    private Dictionary<StateType, CollectorState> _states = new Dictionary<StateType, CollectorState>();

    private AnimationController _animationController;

    private CollectorBotTask _currentTask;
    private CollectorState _currentState;
    private CollectorState _idleState;

    private ICollectable _storage;

    public event Action<CollectorBot> Freed;

    public bool IsFree => _tasks.Count <= 0;

    public Transform Transform => transform;
    public ICollectable Storage => _storage;
    public CollectorBotTask CurrentTask => _currentTask;
    public AnimationController AnimationController => _animationController;

    public void Awake()
    {
        _states[StateType.Idle] = new Idle();
        _states[StateType.Moving] = new MovingState();
        _states[StateType.Taking] = new TakingState();
        _states[StateType.Dropping] = new DroppingState();
        _states[StateType.Mining] = new MiningState();

        _idleState = new Idle();
        _currentState = _idleState;

        _animationController = GetComponent<AnimationController>();

        _currentState.Entry(this);
    }

    private void Update()
    {
        bool isCompleted = _currentState.IsComplete();

        if (isCompleted == false)
        {
            _currentState.Run();
        }
        else
        {
            if (IsFree == false)
            {
                _currentTask = _tasks.Dequeue();
                SwitchToState(_states[_currentTask.StateType]);

                return;
            }
            
            SwitchToState(_idleState);
            Freed?.Invoke(this);
        }
    }

    public void AssignTask(Queue<CollectorBotTask> tasks)
    {
        _tasks = new Queue<CollectorBotTask>(tasks);
    }

    public void SetStoredItem(ICollectable item)
    {
        _storage = item;

        item.Transform.SetParent(transform);
        item.Transform.localPosition = Vector3.zero;
    }

    public void DropItem()
    {
        _storage.Transform.SetParent(null);
        _storage.OnDropped();
        _storage = null;
    }

    private void SwitchToState(CollectorState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Entry(this);
    }
}
