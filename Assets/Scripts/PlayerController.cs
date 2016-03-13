using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    CharacterController characterController;

	// Use this for initialization
	void Start ()
    {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0f,
                                             Input.GetAxis("Vertical"));
        characterController.Move(transform.rotation * movementVector * speed
                                 * Time.deltaTime);
	}
}
