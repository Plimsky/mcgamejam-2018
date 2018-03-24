using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Controller : MonoBehaviour {
	
	private Slider slider;
	private Vector3 startPos;
	private Transform player;
	private Transform abyss; 
	private float fps = 20;

	public Transform endPos;
	public Image relevant;
	public Sprite[] frames;

	public GameObject hook; 

	public Transform[] spawns;

	public Color claimedColour;

	private bool flagOne, flagTwo, flagThree;
	private Image oneQuarter, oneHalf, threeQuarter;
	void Start () 
	{
		slider = transform.GetComponentInChildren<Slider> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		startPos = player.position;
	    endPos = GameObject.FindGameObjectWithTag ("EndMarker").transform;

		abyss = GameObject.FindGameObjectWithTag ("Abyss").transform;


		flagOne = false; 
		flagTwo = false; 
		flagThree = false;
		oneQuarter = (Image)GameObject.Find ("OneQuarterMarker").GetComponent<Image>();
		oneHalf = (Image)GameObject.Find ("HalfWayMarker").GetComponent<Image>();
		threeQuarter = (Image)GameObject.Find ("ThreeQuartersMarker").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		relevant.sprite = frames [(int)(Time.time * fps) % frames.Length];
		slider.value = (player.position.x - startPos.x) / (endPos.position.x - startPos.x) * 4;

		if (slider.value >= .25f && !flagOne) 
		{
			flagOne = true;
			Destroy (oneQuarter.gameObject);
			abyss.GetComponent<FollowerDeadZone>().Speed = 3.0f;
			foreach (Transform spawn in spawns) {
				Instantiate(hook, spawn.transform.position, spawn.transform.rotation); 
			}
			
		}
		else if (slider.value >= .5f && !flagTwo) 
		{
			flagTwo = true;
			Destroy (oneHalf.gameObject);
		} 
		else if (slider.value >= .75f && !flagThree) 
		{
			flagThree = true;
			Destroy (threeQuarter.gameObject);
		}
	}
}
