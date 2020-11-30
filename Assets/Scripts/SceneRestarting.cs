using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarting : MonoBehaviour
{
    [SerializeField] private Player _player;

    private bool _playerIsDead;

    // Update is called once per frame
    private void Update()
    {
        _playerIsDead = _player.isDead;

        if (_playerIsDead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    }
}
