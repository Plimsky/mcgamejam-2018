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
                Animator anim = other.gameObject.GetComponent<Animator>(); 
                PlayerController pc = other.gameObject.GetComponent<PlayerController>();
                if(!pc.isDead) {
                    anim.SetTrigger("Death");
                    pc.isDead = true; 
                }
                anim.SetBool("DeathBool", true); 
                
                //LevelManager.Instance.PlayerDied();

            }
        }
    }
}