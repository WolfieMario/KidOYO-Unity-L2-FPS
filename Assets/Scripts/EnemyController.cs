using UnityEngine;
using System.Collections;

public class EnemyController : DeathController
{
    Animator animator;
    GameObject player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    }

    void OnAnimatorIK(int layer)
    {
        animator.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    }


    public override void OnDeath()
    {
        GameObject ragdoll = transform.parent.FindChild("Ragdoll").gameObject;
        Object.Destroy(this.gameObject);
        ragdoll.SetActive(true);
    }
}
