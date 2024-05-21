using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : Spawner
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _spawnDelay;

    private ObjectPool<Cube> _pool;
    private bool _isSpawning;


    public event UnityAction<Cube> Spawned;
    public override event UnityAction ObjectsNumberChanged;

    private void Start()
    {
        _pool = new ObjectPool<Cube>(_prefab, _poolSize, transform);
        _isSpawning = true;
        StartCoroutine(nameof(RandomPositionSpawning));
    }

    private IEnumerator RandomPositionSpawning()
    {
        WaitForSeconds delay = new(_spawnDelay);
        Vector3 randomPosition;

        while (_isSpawning)
        {
            Cube cube = _pool.GetFreeObject();
            randomPosition = new Vector3(+Random.Range(-_spawnRadius, _spawnRadius), transform.position.y, transform.position.z + Random.Range(-_spawnRadius, _spawnRadius));
            cube.transform.position = randomPosition;
            cube.gameObject.SetActive(true);
            Spawned?.Invoke(cube);
            UpdateInfo();

            yield return delay;
        }
    }

    private void UpdateInfo()
    {
        _objectsSpawned++;
        _objectsActive = _pool.GetActiveObjectsNumber();
        ObjectsNumberChanged?.Invoke();
    }
}