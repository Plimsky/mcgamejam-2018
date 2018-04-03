using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : MonoBehaviour {
	float timeToLog = 0.0f;
	float deltaTime = 1.5f;
	// Use this for initialization
	void Start () {
		//Debug.Log(gameObject.GetComponent<BoxCollider2D>().bounds.center);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeToLog) {
			timeToLog = timeToLog + deltaTime;
			Debug.Log(gameObject.GetComponent<BoxCollider2D>().bounds.center);
		}
		
	}
}
