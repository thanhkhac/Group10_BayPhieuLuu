using System;
using ThanhNK;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHitBox : MonoBehaviour
    {
        private Animator animator;
        private PlayerControl playerControl;
        [SerializeField] public GameObject player;
        private void Awake()
        {
            animator = player.GetComponent<Animator>();
            playerControl = player.GetComponent<PlayerControl>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyAttack") && !playerControl.isImmortal)
            {
                animator.SetBool("isHit", true);
                animator.SetTrigger("Hit");
                PlayerData.PLayerHealth -= 10;
                if (PlayerData.PLayerHealth < 0) { PlayerData.PLayerHealth = 0; }
                playerControl.UpdateHealthBar();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("EnemyAttack") && !playerControl.isImmortal)
            {
                animator.SetBool("isHit", true);
                animator.SetTrigger("Hit");
                PlayerData.PLayerHealth -= 10;
                if (PlayerData.PLayerHealth < 0) { PlayerData.PLayerHealth = 0; }
                playerControl.UpdateHealthBar();
            }
        
            if (other.collider.CompareTag("BloodBottle") && !playerControl.isImmortal)
            {
                PlayerData.PLayerHealth += 10;
                if (PlayerData.PLayerHealth > 100) { PlayerData.PLayerHealth = 100; }
                playerControl.UpdateHealthBar();
            }

            if (other.collider.CompareTag("ManaBottle") && !playerControl.isImmortal)
            {
                PlayerData.PLayerMana += 20;
                if (PlayerData.PLayerMana > 100) { PlayerData.PLayerMana = 100; }
                playerControl.UpdateManaBar();
            }
        }
    }
}