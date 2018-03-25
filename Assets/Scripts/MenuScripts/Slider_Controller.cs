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
	public float progressSpeed = 3.5f; 

	private GameObject[] hooks;
	private GameObject[] teethSpawn; 
	private GameObject[] eyeSpawn;

	public GameObject teethPrefab; 
	public GameObject eyePrefab; 

	//private float timeToStrike; 
	//private float strikeRate = 4.0f; 

	public Color claimedColour;

	private bool flagOne, flagTwo, flagThree;
	private Image oneQuarter, oneHalf, threeQuarter;

	void Start() 
	{
		slider = transform.GetComponentInChildren<Slider> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		startPos = player.position;
	    endPos = GameObject.FindGameObjectWithTag ("EndMarker").transform;

		abyss = GameObject.FindGameObjectWithTag ("Abyss").transform;

		teethSpawn = GameObject.FindGameObjectsWithTag("TeethSpawn"); 
		eyeSpawn = GameObject.FindGameObjectsWithTag("EyeSpawn"); 
		hooks = GameObject.FindGameObjectsWithTag("Hook"); 
		
		flagOne = false; 
		flagTwo = false; 
		flagThree = false;
		oneQuarter = (Image)GameObject.Find ("OneQuarterMarker").GetComponent<Image>();
		oneHalf = (Image)GameObject.Find ("HalfWayMarker").GetComponent<Image>();
		threeQuarter = (Image)GameObject.Find ("ThreeQuartersMarker").GetComponent<Image>();
	}

    void OnLevelWasLoaded()
    {
			Debug.Log("Level has loaded");
			slider = transform.GetComponentInChildren<Slider>();
			player = GameObject.FindGameObjectWithTag("Player").transform;
			startPos = player.position;
			endPos = GameObject.FindGameObjectWithTag("EndMarker").transform;
			abyss = GameObject.FindGameObjectWithTag("Abyss").transform;

			teethSpawn = GameObject.FindGameObjectsWithTag("TeethSpawn"); 
			eyeSpawn = GameObject.FindGameObjectsWithTag("EyeSpawn"); 
			hooks = GameObject.FindGameObjectsWithTag("Hook"); 

			StopAllCoroutines();
			
			flagOne = false;
			flagTwo = false;
			flagThree = false;

        //oneQuarter = (Image)GameObject.Find("OneQuarterMarker").GetComponent<Image>();
        //oneHalf = (Image)GameObject.Find("HalfWayMarker").GetComponent<Image>();
        //threeQuarter = (Image)GameObject.Find("ThreeQuartersMarker").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () 
	{
		
		relevant.sprite = frames [(int)(Time.time * fps) % frames.Length];
		slider.value = (player.position.x - startPos.x) / (endPos.position.x - startPos.x) * progressSpeed;
	   

		if (slider.value >= .25f && !flagOne) 
		{
			flagOne = true;
			StartCoroutine("SpawnCoroutine");
			//Destroy (oneQuarter.gameObject);
			abyss.GetComponent<FollowerDeadZone>().Speed = 3.6f;
			
		}
		else if (slider.value >= .5f && !flagTwo) 
		{
			flagTwo = true;
			StopAllCoroutines(); 
			spawnTeeth();
			StartCoroutine("SpeedBite");
			//Destroy (oneHalf.gameObject);
			//abyss.GetComponent<FollowerDeadZone>().Speed = 3.7f;
		} 
		else if (slider.value >= .75f && !flagThree) 
		{
			flagThree = true;
			spawnEyes(); 
			//Destroy (threeQuarter.gameObject);
		}
	}

	void spawnTeeth() {

		foreach(GameObject spawn in teethSpawn) {
			var teeth = Object.Instantiate(teethPrefab, spawn.transform.position, spawn.transform.rotation); 
			teeth.transform.parent = spawn.transform.parent; 
		}
	}

	void spawnEyes() {
		foreach(GameObject spawn in eyeSpawn) {
			var eye = Object.Instantiate(eyePrefab, spawn.transform.position, spawn.transform.rotation); 
			eye.transform.parent = spawn.transform.parent; 
		}
	}
	IEnumerator SpeedBite() {
		float timeToCharge = 4.0f; 
		float timeToSlow = 1.0f; 

		while (true) {
			abyss.GetComponent<FollowerDeadZone>().Speed = 3.5f;
			yield return new WaitForSeconds(timeToCharge); 

			abyss.GetComponent<FollowerDeadZone>().Speed = 5.0f;  
			yield return new WaitForSeconds(timeToSlow);
		}
	}

	IEnumerator SpawnCoroutine() {
		float waitTime = 0.35f;
		float betweenLoops = 0.75f;  
		while (true) {
			for (int i = 0; i < 4; i++) {
				Animator anim;
				anim = hooks[i].transform.GetComponent<Animator>();
				Vector2 newPos = new Vector2 (hooks[i].transform.position.x, player.position.y); 
				Transform hookTransform = hooks[i].transform; 
			
				hookTransform.rotation =  Quaternion.Euler(0, 0, Random.Range(-10f, 10f)); 
				newPos.y = newPos.y + Random.Range(-0.5f, 0.5f); 
				newPos.x = newPos.x += Random.Range(0.0f, 0.5f); 
				
				hooks[i].transform.position = newPos; 
				anim.SetTrigger("pierceTrigger"); 
				yield return new WaitForSeconds(waitTime); 
			}
			yield return new WaitForSeconds(betweenLoops); 
		}
	}


}
