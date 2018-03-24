using UnityEngine;

namespace System
{
    public class UiInGameManager : MonoBehaviour
    {
        public GameObject DiedText;

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

        public void StartLevel()
        {
            DiedText.SetActive(false);
        }

        public void EndLevel(bool levelcomplete)
        {
            DiedText.SetActive(false);
        }

        public void PlayerDied()
        {
            DiedText.SetActive(true);
        }
    }
}