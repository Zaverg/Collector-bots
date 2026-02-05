using UnityEngine;
using System.Collections.Generic;

public class CollectorBotDispatcher : MonoBehaviour
{
    [SerializeField] private List<CollectorBot> _collectors = new List<CollectorBot>();

    private Queue<CollectorBot> _availableCollectors;

    public void Initialize()
    {
        foreach (CollectorBot collectorBot in _collectors)
        {
            collectorBot.Initialize();
        }
    }
}