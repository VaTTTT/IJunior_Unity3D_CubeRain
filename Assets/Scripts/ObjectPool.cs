using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;

    public ObjectPool(T prefab, int poolSize, Transform container)
    {
        Prefab = prefab;
        Container = container;

        CreatePool(poolSize);
    }

    private T Prefab { get; }
    private Transform Container { get; }

    public T GetFreeObject()
    {
        T freeObject = _pool.FirstOrDefault(freeObject => !freeObject.isActiveAndEnabled);

        return freeObject;
    }

    public int GetActiveObjectsNumber()
    {
        return _pool.Count(activeObject => activeObject.isActiveAndEnabled);
    }

    private void CreatePool(int poolSize)
    {
        _pool = new List<T>();

        for (int i = 0; i < poolSize; i++)
        {
            T newObject = Object.Instantiate(Prefab, Container);
            _pool.Add(newObject);
            newObject.gameObject.SetActive(false);
        }
    }
}