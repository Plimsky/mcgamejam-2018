using UnityEngine;

namespace System
{
    public class UiInGameManager : MonoBehaviour
	{
		public GameObject DiedText;
		public GameObject PauseUI;

        public static UiInGameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
		void Update()
		{ 
			if (Input.GetKeyDown (KeyCode.P) && DiedText.activeSelf == false) 
			{
				if (Time.timeScale != 0.0f)  
				{ 
					PauseLevel (); 
				} 
				else 
				{ 
					ResumeLevel (); 
				} 
			} 
		} 
		public void PauseLevel() 
		{ 
			Time.timeScale = 0.0f; 
			PauseUI.SetActive (true); 

		}   
		public void ResumeLevel() 
		{ 
			Time.timeScale = 1.0f; 
			PauseUI.SetActive (false); 
		} 

        public void StartLevel()
        {
			DiedText.SetActive(false);
			PauseUI.SetActive (false); 
        }

        public void EndLevel(bool levelcomplete)
        {
			DiedText.SetActive(false);
			PauseUI.SetActive (false); 
        }

        public void PlayerDied()
        {
            DiedText.SetActive(true);
        }
    }
}