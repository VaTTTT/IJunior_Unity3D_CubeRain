using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initialNumberOfCubes;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnRadius;

    private List<Cube> _cubePool;
    private bool _isSpawning;

    private void Start()
    {
        _cubePool = new List<Cube>();
        CreateCubePool();
        _isSpawning = true;
        StartCoroutine(nameof(SpawnCubes));
    }

    private void CreateCubePool()
    {
        for (int i = 0; i < _initialNumberOfCubes; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, transform.position, transform.rotation);
            _cubePool.Add(newCube);
            newCube.gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);
        Vector3 spawnPosition;

        while (_isSpawning)
        {
            foreach (Cube cube in _cubePool)
            {
                if (!cube.gameObject.activeSelf)
                {
                    spawnPosition = new Vector3(transform.position.x + Random.Range(-_spawnRadius, _spawnRadius), transform.position.y, transform.position.z + Random.Range(-_spawnRadius, _spawnRadius));

                    cube.SetPosition(spawnPosition);
                    cube.gameObject.SetActive(true);

                    yield return delay;
                }
            }

            yield return null;
        }
    }
}