using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gem : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(PickUp());
        }
    }

    private IEnumerator PickUp()
    {
        _animator.SetTrigger("Picked");

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
