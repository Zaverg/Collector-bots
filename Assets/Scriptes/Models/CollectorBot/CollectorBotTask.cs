using UnityEngine;

public struct CollectorBotTask
{
    public StateType StateType { get; private set; }
    public Transform Target { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public Cell Cell { get; private set; }

    public CollectorBotTask(StateType stateType, Vector3 targetPosition = default, Transform target = null, Cell cell = null)
    {
        StateType = stateType;
        TargetPosition = targetPosition;
        Target = target;
        Cell = cell;
    }
}
