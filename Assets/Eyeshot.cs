using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeshot : MonoBehaviour {
	public float speed = 3.0f;
	private Vector3 direction;
	private GameObject player;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		direction = player.transform.position - transform.position;
		direction.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(direction * Time.deltaTime * speed);
	}
}
