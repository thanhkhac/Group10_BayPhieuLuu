using System;
using ThanhNK;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class  PlayerHitBox : MonoBehaviour
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
                animator.SetTrigger("Hit");
                GameManager.PLayerHealth -= 10;
                Debug.Log(GameManager.PLayerHealth);
            }
        }
    }
}