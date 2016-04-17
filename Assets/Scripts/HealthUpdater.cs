using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUpdater : MonoBehaviour {
	public Health playerHealth;
	
	Slider slider;

	// Use this for initialization
	void Awake () {
		slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = playerHealth.health / playerHealth.maxHealth;
	}
}
