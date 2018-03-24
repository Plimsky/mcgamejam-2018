using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Play_onClick : MonoBehaviour {

	public string levelName;

	void Start(){
		Button b = transform.GetComponent<Button> ();
		b.onClick.AddListener (LoadLevel);
	}
	void LoadLevel(){
		Debug.Log (levelName);
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
	}
}
