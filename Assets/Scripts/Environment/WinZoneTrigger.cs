using System;
using UnityEngine;

namespace Environment
{
    public class WinZoneTrigger : MonoBehaviour
    {
        private GameObject UIslider;
        public string TagName = "Player";

        private void Start() {
            UIslider = GameObject.FindGameObjectWithTag("UI");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagName))
            {
                UIslider.SetActive(false);
                LevelManager.Instance.PlayerWin();
            }
        }
    }
}