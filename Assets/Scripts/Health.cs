using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    DeathController deathController;

    void Awake()
    {
        health = maxHealth;
        deathController = GetComponent<DeathController>();
    }
	
    void Update()
    {
        if (transform.position.y < -20)
            health--;

        if (health <= 0)
            deathController.OnDeath();
    }
}
