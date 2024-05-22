using UnityEngine;
using UnityEngine.Events;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected int _poolSize;

    protected int _objectsSpawned;
    protected int _objectsActive;

    public int ObjectsSpawned => _objectsSpawned;
    public int ObjectsActive => _objectsActive;

    public abstract event UnityAction ObjectsNumberChanged;
}