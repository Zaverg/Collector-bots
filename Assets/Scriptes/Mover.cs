using UnityEngine;
using UnityEngine.AI;

public class Mover : State
{
    private NavMeshAgent _agent;
    private IStateMachine _stateMachine;

    private Vector3 _target;
    private float _distance = 4f;

    private bool _isMoving = false;

    public Mover(Vector3 target)
    { 
        _target = target;
    }

    public override void Entry(IStateMachine stateMachine)
    {
        _agent = stateMachine.Transform.GetComponent<NavMeshAgent>();
        _stateMachine = stateMachine;
    }

    public override void Run()
    {
        if (_isMoving == false && _agent != null)
        {
            _isMoving = true;

            _agent.SetDestination(_target);
            _stateMachine.AnimationController.SetMoveAnimation(_isMoving);
        }
    }

    public override void Exit()
    {
        _agent.ResetPath();
        _isMoving = false;
        _agent = null;

        _stateMachine.AnimationController.SetMoveAnimation(_isMoving);
        _stateMachine = null;
    }

    public override bool TryTransitionState()
    {
        if ((_target - _stateMachine.Transform.position).sqrMagnitude <= _distance * _distance)
            return true;

        return false;
    }
}