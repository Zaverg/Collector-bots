using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(UnitAnimator))]
public class CollectorBot : MonoBehaviour, IStateMachine
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Taker _taker;
    [SerializeField] private Mining _mining;

    private Queue<CollectorBotTask> _tasks = new Queue<CollectorBotTask>();
    private Dictionary<StateType, CollectorState> _states = new Dictionary<StateType, CollectorState>();

    private UnitAnimator _animationController;

    private CollectorBotTask _currentTask;
    private CollectorState _currentState;

    public event Action<CollectorBot> Freed;

    public bool HasTask => _tasks.Count > 0;

    public IMoveble Mover => _mover;
    public ITaker Taker => _taker;
    public IMining Mining => _mining;

    public Transform Transform => transform;
    public CollectorBotTask CurrentTask => _currentTask;
    public UnitAnimator AnimationController => _animationController;

    public void Initialize()
    {
        _states[StateType.Idle] = new Idle();
        _states[StateType.Moving] = new MovingState();
        _states[StateType.Taking] = new TakingState();
        _states[StateType.Dropping] = new DroppingState();
        _states[StateType.Mining] = new MiningState();

        _currentState = _states[StateType.Idle];

        _animationController = GetComponent<UnitAnimator>();

        _currentState.Entry(this);
    }

    private void Update()
    {
        bool isCompleted = _currentState.IsComplete();

        if (isCompleted)
        {
            if (HasTask)
            {
                _currentTask = _tasks.Dequeue();
                SwitchToState(_states[_currentTask.StateType]);

                return;
            }

            SwitchToState(_states[StateType.Idle]);
            Freed?.Invoke(this);
        }
        else
        {
            _currentState.Run(); 
        }
    }

    public void AssignTasks(Queue<CollectorBotTask> tasks)
    {
        _tasks = new Queue<CollectorBotTask>(tasks);
    }

    private void SwitchToState(CollectorState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Entry(this);
    }
}