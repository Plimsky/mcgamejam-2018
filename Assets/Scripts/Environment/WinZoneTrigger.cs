using System;
using UnityEngine;

namespace Environment
{
    public class WinZoneTrigger : MonoBehaviour
    {
        public string TagName = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagName))
            {
                LevelManager.Instance.PlayerWin();
            }
        }
    }
}