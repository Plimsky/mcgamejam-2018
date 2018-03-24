using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Controller : MonoBehaviour {
	
	private Slider slider;
	private Vector3 startPos;
	private Transform player;
	private float fps = 20;

	public Transform endPos;
	public Image relevant;
	public Sprite[] frames;

	void Start () {
		slider = transform.GetComponentInChildren<Slider> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		startPos = player.position;
		endPos = GameObject.FindGameObjectWithTag ("EndMarker").transform;
	}
	
	// Update is called once per frame
	void Update () {
		relevant.sprite = frames[(int)(Time.time * fps) % frames.Length];
		Debug.Log (startPos.x + " " + player.position.x + " " + endPos.position.x);
		slider.value = (player.position.x - startPos.x) / (endPos.position.x - startPos.x);
	}
}
