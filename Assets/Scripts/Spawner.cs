using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _spawnObjectPrefab;

    private List<Transform> _spawnPoints = new List<Transform>();
    private void Start()
    {
        for (int i = 0; i < _spawnPoint.childCount; i++)
        {
            _spawnPoints.Add(_spawnPoint.GetChild(i));
        }

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            Instantiate(_spawnObjectPrefab, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}
