using UnityEngine;
using System.Collections;

public class EnemyController : DeathController
{
    public Gun gun;
    public float shootDistance = 10;

    Animator animator;
    NavMeshAgent agent;
    Rigidbody rigidbody;

    GameObject player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = transform.parent.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        agent.updatePosition = false;
    }
	
    // Update is called once per frame
    void Update()
    {
        /*Quaternion rotation = transform.parent.rotation;
        Vector3 direction = player.transform.position -
                            transform.parent.position;
        direction.y = 0;
        rotation.SetLookRotation(direction, Vector3.up);
        transform.parent.rotation = rotation;*/

        EnemyMovement();

        gun.cooldown -= Time.deltaTime;
        if (gun.cooldown <= 0)
        {
            Shoot();
            gun.cooldown = gun.shootCooldown;
        }
    }

    void OnAnimatorIK(int layer)
    {
        animator.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    }

    void EnemyMovement()
    {
        /*transform.parent.position += transform.parent.forward * Time.deltaTime;
        Vector3 position = transform.parent.position;
        position.y += 0.1f;
        transform.parent.position = position;
        */
        if ((player.transform.position - rigidbody.position).sqrMagnitude >
            shootDistance*shootDistance) {
            agent.SetDestination(player.transform.position);
        }
        else {
            agent.SetDestination(rigidbody.position);
        }
        rigidbody.velocity = agent.velocity;
        agent.nextPosition = rigidbody.position;
    }

    void Shoot()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(gun.shootPoint.position, gun.shootPoint.forward, out hitInfo);
        if (hit) {
            GameObject target = hitInfo.collider.gameObject;
            Health health = target.GetComponent<Health>();
            if (health != null) {
                health.health -= gun.damage;
                Rigidbody rigidbody = target.transform.gameObject.GetComponent<Rigidbody>();
                if (rigidbody != null) {
                    rigidbody.AddForce(gun.shootPoint.forward, ForceMode.Impulse);
                }
            }
        }

        GameObject bullet = (GameObject)GameObject.Instantiate(gun.bullet,
            gun.shootPoint.position, gun.shootPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(gun.shootPoint.forward * gun.damage, ForceMode.VelocityChange);
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(gun.shootPoint.position, gun.shootPoint.forward * 20);
    }

    public override void OnDeath()
    {
        GameObject ragdoll = gameObject.transform.parent.FindChild("Ragdoll").gameObject;
        Object.Destroy(gameObject);
        ragdoll.SetActive(true);
    }
}
