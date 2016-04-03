using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{

    new Camera camera;

	// Use this for initialization
	void Awake ()
    {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Fire1") > 0) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo);
            if (hit) {
                GameObject enemy = hitInfo.transform.gameObject;
                if (enemy.tag == "Enemy") {
                    GameObject ragdoll = enemy.transform.parent.FindChild("Ragdoll").gameObject;
                    Object.Destroy(enemy);
                    ragdoll.SetActive(true);
                }
            }
        }
	}
}
