using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void Look()
    {
        Quaternion rotation = transform.parent.rotation;
        Vector3 direction = player.transform.position - transform.parent.position;
        direction.y = 0;
        rotation.SetLookRotation(direction, Vector3.up);
        transform.parent.rotation = rotation;
    }

    void OnAnimatorIK(int index)
    {
        animator.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    }
}
