using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {
	float rateToFire = 3.0f;
	private Animator animator;
	public GameObject eyeshotPrefab;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		StartCoroutine("Fire"); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Fire() {
		while (true) {
			float chance = Random.Range(0.0f, 1.0f);
			if (chance >= .5) {
				animator.SetTrigger("Attack");
			}
			yield return new WaitForSeconds(rateToFire); 
		}
	}

	public void spawnShot() {
		Transform spawnPos = transform.GetChild(0);
		Object.Instantiate(eyeshotPrefab, spawnPos.position, spawnPos.rotation);
	}
}
