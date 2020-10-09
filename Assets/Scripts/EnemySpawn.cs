using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemyPrefab;

    private List<Transform> _spawnPoints = new List<Transform>();
    void Start()
    {
        for (int i = 0; i < _spawnPoint.childCount; i++)
        {
            _spawnPoints.Add(_spawnPoint.GetChild(i));
            Debug.Log(_spawnPoints[i].transform.name);
        }

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            Instantiate(_enemyPrefab, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}
