using UnityEngine;

namespace System
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        #region Events

        public delegate void LevelStartHandler();

        public static event LevelStartHandler OnLevelStart;

        public delegate void PlayerDiedHandler();

        public event PlayerDiedHandler OnPlayerDied;

        public delegate void LevelEndHandler(bool levelComplete);

        public static event LevelEndHandler OnLevelEnd;

        #endregion

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

            OnLevelStart += SetupLevel;
            OnLevelEnd += ClearLevel;
            OnPlayerDied += SessionManager.Instance.ReloadActualScene;
        }

        public void StartLevel()
        {
            if (OnLevelStart != null)
                OnLevelStart();
        }

        public void PlayerDied()
        {
            Debug.Log("Player Died");
            if (OnPlayerDied != null)
                OnPlayerDied();
        }

        public void PlayerWin()
        {
            Debug.Log("Player Win");
            EndLevel(true);
        }

        public void EndLevel(bool levelComplete)
        {
            if (OnLevelEnd != null)
                OnLevelEnd(levelComplete);
        }

        private void SetupLevel()
        {
            Debug.Log("Setup Level");
        }

        private void ClearLevel(bool levelComplete)
        {
            Debug.Log("Clear Level");

            if (levelComplete)
                SessionManager.Instance.LoadNextScene();
        }
    }
}