using UnityEngine;

public abstract class Mining : MonoBehaviour, IMining
{
    public abstract bool IsComplete { get; }

    public abstract void SetDiration(float duration);

    public abstract void StartMining();
}
