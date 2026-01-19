using System.Collections;
using UnityEngine;

public class CoroutineStarter : MonoBehaviour
{
    public static CoroutineStarter Instance { get; private set; }

    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Coroutine StartChildCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void StopChildCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}