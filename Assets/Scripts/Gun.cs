using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float damage = 10;
    public float shootCooldown = 2;
    public float cooldown;

    void Awake()
    {
        cooldown = shootCooldown;
    }
}
