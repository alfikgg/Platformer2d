using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PhysicsMovement))]

public class Player : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private PhysicsMovement _physicsMovement;

    public bool isDead { get; private set; }

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _physicsMovement = GetComponent<PhysicsMovement>();
        isDead = false;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<Enemy>() || collision2D.gameObject.GetComponent<DeathZone>())
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        isDead = true;
        _boxCollider2D.isTrigger = true;
        _physicsMovement.enabled = false;

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        SceneManager.LoadSceneAsync(0);
    }

}
