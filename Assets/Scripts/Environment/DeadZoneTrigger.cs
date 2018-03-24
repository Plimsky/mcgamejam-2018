using System;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

namespace Environment
{
    public class DeadZoneTrigger : MonoBehaviour
    {
        public string TagName = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagName))
            {
                LevelManager.Instance.PlayerDied();
            }
        }
    }
}