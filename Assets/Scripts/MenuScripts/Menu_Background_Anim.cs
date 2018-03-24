using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Background_Anim : MonoBehaviour {

	//could either store the individual frames, or do something fancy
	//gonna store individual

	//simple four frame flicker, on a brick background or some shit
	//for now, it's temporary black & white images

	public Texture2D[] frames;
	int fps = 5;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int index = (frames.Length > 0) ? (int)(Time.time * fps) % frames.Length : 0;
		transform.GetComponent<CanvasRenderer> ().SetTexture (frames[index]);
	}
}
