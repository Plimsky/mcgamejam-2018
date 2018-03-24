using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits_Scroll : MonoBehaviour {

	public Text[] words;
	public float scrollSpeed;
	public Image background;
	public float rateOfAlphaChange;
	bool done;

	// Use this for initialization
	void Start () {
		done = false;
		background = GetComponent<Image> ();
		for (int i = 0; i < words.Length; i++) {
			words [i].rectTransform.Translate (-Vector3.up * 650);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!done) {
			for (int i = 0; i < words.Length; i++) {
				words [i].rectTransform.Translate (Vector3.up * scrollSpeed * Time.deltaTime);
				if (words [i].rectTransform.anchoredPosition.y >= 130) {
					done = true;
				}
			}
		}
		Color tempColor = background.color;
		tempColor.a += rateOfAlphaChange * Time.deltaTime;
		Debug.Log (tempColor.a);
		background.color = tempColor;
	}
}
