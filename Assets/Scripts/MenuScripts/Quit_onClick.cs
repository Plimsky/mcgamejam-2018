using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Quit_onClick : MonoBehaviour {

	void Start(){
		Button b = transform.GetComponent<Button> ();
		b.onClick.AddListener (Quit);
	}

	void Quit(){
		Debug.Log ("A");
		Application.Quit ();
		Debug.Log ("B");
	}
}
