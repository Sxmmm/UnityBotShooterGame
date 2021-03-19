using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start() { Destroy(gameObject, 5f); }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());

        Destroy(gameObject);
    }
}
