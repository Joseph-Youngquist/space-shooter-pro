using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private float _spawnRateMin = 1.0f;
    [SerializeField]
    private float _spawnRateMax = 3.5f;
    [SerializeField]
    private bool _spawningAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("SpawnManager::Start() - Enemy Prefab is NULL");
        }

        if (_enemyContainer == null)
        {
            Debug.LogError("SpawnManager::Start() - Enemy Container is NULL");
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (_spawningAllowed)
        {
            float randomX = Random.Range(-9.0f, 9.0f);

            Vector3 newEnemyPosition = new Vector3(randomX, 7.0f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, newEnemyPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(_spawnRateMin, _spawnRateMax));
        }
    }

    public void OnPlayerDeath(bool allowed)
    {
        _spawningAllowed = allowed;
    }
}
