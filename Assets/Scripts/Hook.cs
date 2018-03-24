using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
	private Animator anim; 
	public float strikeRateMin = 3.0f; 
	public float strikeRateMax = 5.0f; 
	private float timeToStrike; 

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.FindGameObjectWithTag("Abyss").transform;
		anim = GetComponent<Animator>(); 
	}

	void Update() {
		if (Time.time > timeToStrike) {
			anim.SetTrigger("pierceTrigger"); 
			timeToStrike = Time.time + Random.Range(strikeRateMin, strikeRateMax);
		} 
	}
	

}
