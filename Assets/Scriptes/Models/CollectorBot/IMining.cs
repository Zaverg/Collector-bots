public interface IMining
{
    public bool IsComplete { get; }
    public void SetDiration(float duration);
    public void StartMining();
}
