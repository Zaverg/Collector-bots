using UnityEngine;

public abstract class Mover : MonoBehaviour, IMoveble
{
    public abstract bool IsPlace();

    public abstract void Move();

    public abstract void SetTarget(Vector3 target);
}