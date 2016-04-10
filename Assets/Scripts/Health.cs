using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    DeathController deathController;

    // Use this for initialization
    void Awake()
    {
        health = maxHealth;
        deathController = GetComponent<DeathController>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
            health -= Time.deltaTime * 60;

        if (health <= 0)
            deathController.OnDeath();
    }
}
