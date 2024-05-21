using UnityEngine;
using UnityEngine.Events;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _prefab;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private ObjectPool<Bomb> _pool;

    public override event UnityAction ObjectsNumberChanged;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += OnCubeDisabled;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= OnCubeDisabled;
    }

    private void Start()
    {
        _pool = new ObjectPool<Bomb>(_prefab, _poolSize, transform);
    }

    private void OnCubeDisabled(Cube cube)
    {
        cube.Disabled += SpawnBomb;
    }

    private void SpawnBomb(Cube cube)
    {
        cube.Disabled -= SpawnBomb;
        Bomb bomb = _pool.GetFreeObject();
        bomb.transform.position = cube.transform.position;
        bomb.gameObject.SetActive(true);
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        _objectsSpawned++;
        _objectsActive = _pool.GetActiveObjectsNumber();
        ObjectsNumberChanged?.Invoke();
    }
}