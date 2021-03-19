using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health = 100.0f;

    private float startHealth;
    private float damage = 30;

    private void Start()
    {
        startHealth = health;
    }

    private void takeDamage()
    {
        health -= damage;

        if (health <= 0.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            takeDamage();
    }
}
