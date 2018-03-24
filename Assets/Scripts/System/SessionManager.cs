using UnityEngine;
using UnityEngine.SceneManagement;

namespace System
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance;

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

            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name != "MainMenu")
            {
                LevelManager.Instance.StartLevel();
            }
        }

        public void ReloadActualScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}