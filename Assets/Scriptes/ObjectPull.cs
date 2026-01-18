using UnityEngine;
using System.Collections.Generic;

public abstract class ObjectPull<T> : MonoBehaviour where T : Component, IReleasable<T>
{
    [SerializeField] private T _prefab;

    private Queue<T> _objectsQueue;
    private List<T> _subscription;

    public void OnDisable()
    {
        _objectsQueue.Clear();

        for (int i = 0; i < _subscription.Count; i++)
        {
            T obj = _subscription[i];
            obj.Released -= PutObject;

            _subscription.Remove(obj);   
        }
    }

    public virtual void Initialization()
    {
        _objectsQueue = new Queue<T>();
        _subscription = new List<T>();
    }

    protected T GetObject()
    {
        T newObject = null;

        if (_objectsQueue.Count == 0)
        {
            newObject = Instantiate(_prefab);
            newObject.Released += PutObject;

            _subscription.Add(newObject);

            return newObject;
        }

        newObject = _objectsQueue.Dequeue();
        newObject.gameObject.SetActive(true);

        return newObject;
    }
     
    protected virtual void PutObject(T obj)
    {
        _objectsQueue.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
