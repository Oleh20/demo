using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> _pool = new();
    private readonly T _prefab;

    public ObjectPool(T prefab, int initialSize)
    {
        _prefab = prefab;
        for (int i = 0; i < initialSize; i++)
        {
            var instance = Object.Instantiate(_prefab);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }

    public T Get()
    {
        T instance = _pool.Count > 0 ? _pool.Dequeue() : Object.Instantiate(_prefab);
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void Return(T instance)
    {
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }
}
