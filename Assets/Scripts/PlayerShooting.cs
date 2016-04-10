using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public float gunDamage = 35f;

    new Camera camera;

	// Use this for initialization
	void Awake ()
    {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0)) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo);
            if (hitInfo.transform != null) {
                GameObject target = hitInfo.transform.gameObject;
                Health health = target.GetComponent<Health>();
                if (health != null) {
                    health.health -= gunDamage;
                }
            }
        }
	}
}
