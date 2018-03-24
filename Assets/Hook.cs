using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.FindGameObjectWithTag("Abyss").transform;
	}
	

}
