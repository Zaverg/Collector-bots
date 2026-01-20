using UnityEngine;
using UnityEngine.AI;

public class MovingState : CollectorState
{
    private NavMeshAgent _agent;
    private IStateMachine _stateMachine;

    private Vector3 _targetPosition;

    private float _stoppingDistance = 0.5f;

    private float _intervalUpdatePath = 0.5f;
    private float _currentSeconds;
    private float _lastUpdateTime;

    public override void Entry(IStateMachine stateMachine)
    {
        _agent = stateMachine.Transform.GetComponent<NavMeshAgent>();
        _stateMachine = stateMachine;
        _agent.stoppingDistance = _stoppingDistance;
        _targetPosition = stateMachine.CurrentTask.TargetPosition;

        _agent.SetDestination(_targetPosition);
        _stateMachine.AnimationController.SetMoveAnimation(true);
    }

    public override void Run()
    {
        if (_agent == null)
            return;

        _currentSeconds += Time.deltaTime;

        if (_currentSeconds - _lastUpdateTime >= _intervalUpdatePath)
        {
            _agent.SetDestination(_targetPosition);
            _lastUpdateTime = _currentSeconds;
        }
    }

    public override void Exit()
    {
        _agent.ResetPath();
        _agent = null;

        _stateMachine.AnimationController.SetMoveAnimation(false);
        _stateMachine = null;
    }

    public override bool IsComplete()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
            return true;

        return false;
    }
}