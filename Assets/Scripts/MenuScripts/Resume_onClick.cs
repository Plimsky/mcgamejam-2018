﻿using System.Collections; 
using System; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class Resume_onClick : MonoBehaviour { 
	void Start(){ 
		Button b = transform.GetComponent<Button> (); 
		b.onClick.AddListener (LoadLevel); 
	} 
	void LoadLevel(){ 
		Debug.Log ("RESUMING"); 
		UiInGameManager.Instance.ResumeLevel (); 
	} 
} 