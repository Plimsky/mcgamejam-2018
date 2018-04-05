using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour {

	SpriteRenderer sprite; 
	float timer = 0.2f;

	GameObject player; 

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>(); 

		player = GameObject.FindGameObjectWithTag("Player"); 
		transform.position = player.transform.position; 
		transform.localScale = player.transform.localScale;

		sprite.sprite = player.GetComponent<SpriteRenderer>().sprite; 
		sprite.color = new Vector4 (50, 50, 50, 0.2f); 
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0)
			Destroy(gameObject); 
	}
}
