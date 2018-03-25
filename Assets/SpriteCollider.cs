using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollider : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent<PolygonCollider2D>(); //collider for itself
		obj2.AddComponent<PolygonCollider2D>(); //collider for other object
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
