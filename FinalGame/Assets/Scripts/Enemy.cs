using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 15f;

    [HideInInspector]
    public float speed;

    public int damage = 5;
    public float health = 50;
    public int value = 5;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;
        PlayerStats.skillPoints += value;

        WaveSpawner.enemiesAlive--;

        Destroy(gameObject);
    }
}