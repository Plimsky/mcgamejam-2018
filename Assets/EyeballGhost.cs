using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballGhost : MonoBehaviour {
	SpriteRenderer sprite; 
	float timer = 0.1f;

	// Use this for initialization

	public void Setup(GameObject obj) {
		sprite = GetComponent<SpriteRenderer>(); 

		transform.position = obj.transform.position;
		transform.localScale = obj.transform.localScale;

		sprite.sprite = obj.GetComponent<SpriteRenderer>().sprite;
		sprite.color = new Vector4 (50, 50, 50, 0.05f);  
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0)
			Destroy(gameObject); 
	}
}
