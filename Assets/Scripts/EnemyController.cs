using UnityEngine;
using System.Collections;

public class EnemyController : DeathController
{
    public Gun gun;
    public float shootCooldown = 2;

    float cooldown;

    Animator animator;
    GameObject player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cooldown = 0;
    }
	
    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = transform.parent.rotation;
        Vector3 direction = player.transform.position -
                            transform.parent.position;
        direction.y = 0;
        rotation.SetLookRotation(direction, Vector3.up);
        transform.parent.rotation = rotation;

        cooldown -= Time.deltaTime;
        if (cooldown <= 0) {
            Shoot();
            cooldown = shootCooldown;
        }
    }

    void OnAnimatorIK(int layer)
    {
        animator.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    }

    void Shoot()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(gun.shootPoint.position, gun.shootPoint.forward, out hitInfo);
        if (hit) {
            GameObject target = hitInfo.collider.gameObject;
            Debug.Log(hitInfo.collider.gameObject);
            Health health = target.GetComponent<Health>();
            if (health != null) {
                Debug.Log(health);
                health.health -= gun.damage;
            }
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(gun.shootPoint.position, gun.shootPoint.forward);
    }


    public override void OnDeath()
    {
        GameObject ragdoll = transform.parent.FindChild("Ragdoll").gameObject;
        Object.Destroy(this.gameObject);
        ragdoll.SetActive(true);
    }
}
