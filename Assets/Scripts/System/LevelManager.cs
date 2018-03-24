using System.Collections;
using UnityEngine;
using Utils;

namespace System
{
    public class LevelManager : MonoBehaviour
    {
        public float TimeToWaitBeforeReloadLevel = 2f;

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
            OnLevelStart += UiInGameManager.Instance.StartLevel;
            OnLevelEnd += ClearLevel;
            OnLevelEnd += UiInGameManager.Instance.EndLevel;
            OnPlayerDied += ReloadLevel;
            OnPlayerDied += UiInGameManager.Instance.PlayerDied;
        }

        private void ReloadLevel()
        {
            StartCoroutine(WaitBeforeReloadLevel());
        }

        private IEnumerator WaitBeforeReloadLevel()
        {
            yield return new WaitForSeconds(TimeToWaitBeforeReloadLevel);
            SessionManager.Instance.ReloadActualScene();
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

            StartCoroutine(Camera.main.gameObject.GetComponent<CameraShake>().Shake(.1f, 1f));
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