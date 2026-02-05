using UnityEngine;

public interface IMoveble
{
    public void SetTarget(Vector3 target);
    public void Move();
    public bool IsPlace();
}