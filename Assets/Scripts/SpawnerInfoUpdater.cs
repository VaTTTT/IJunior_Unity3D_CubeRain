using UnityEngine;

public class SpawnerInfoUpdater : MonoBehaviour
{
    [SerializeField] private Spawner _objectSpawner;
    [SerializeField] private TMPro.TMP_Text _activeObjectsInfo;
    [SerializeField] private TMPro.TMP_Text _totalSpawnedObjectsInfo;

    private void OnEnable()
    {
        _objectSpawner.ObjectsNumberChanged += UpdateInfo;
    }

    private void OnDisable()
    {
        _objectSpawner.ObjectsNumberChanged -= UpdateInfo;
    }

    private void UpdateInfo()
    {
        _activeObjectsInfo.text = _objectSpawner.ObjectsActive.ToString();
        _totalSpawnedObjectsInfo.text = _objectSpawner.ObjectsSpawned.ToString();
    }
}