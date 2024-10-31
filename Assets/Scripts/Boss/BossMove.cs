using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float moveSpeed = 1f; // Tốc độ di chuyển
    public float stopDistance = 1f; // Khoảng cách dừng lại
    private Transform playerTransform; // Biến để lưu Transform của Player
    Animator animator;
    private float delayTime = 5f;
    float delayAtk = 0;
    bool checkRolateBoss = true;
    private System.Random random = new System.Random();
    void Start()
    {

        animator = GetComponent<Animator>();
        // Tìm đối tượng có tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform; // Lưu Transform của Player
        }
        else
        {
            Debug.LogWarning("No Player found with tag 'Player'.");
        }
    }

    void Update()
    {
        Vector3 relativePosition = playerTransform.position - transform.position;
        delayTime += Time.deltaTime;
        if (playerTransform != null)
        {
            // Tính toán hướng di chuyển (bỏ qua thành phần Y)
            Vector3 direction = new Vector3(
                playerTransform.position.x - transform.position.x,
                0, // Bỏ qua trục Y
                playerTransform.position.z - transform.position.z
            ).normalized;

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            Debug.Log(distanceToPlayer);
            // Kiểm tra khoảng cách đến Player
            if (distanceToPlayer > stopDistance)
            {
                // Di chuyển theo hướng của Player
                transform.position += direction * moveSpeed * Time.deltaTime;
                animator.SetBool("IsMove", true);
            }
            if (relativePosition.x < 0 ) // Nếu Player ở phía sau Boss
            {
                if (checkRolateBoss)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                }
                checkRolateBoss = false;
            }
            if (relativePosition.x > 0) // Nếu Player ở phía sau Boss
            {
                if (!checkRolateBoss)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                }
                checkRolateBoss = true;
            }


            if (distanceToPlayer <= stopDistance)
            {
                animator.SetBool("IsMove", false);
                if (delayAtk >= 2f)
                {
                    int randomNumber = random.Next(0, 5);
                    if (randomNumber == 1)
                    {
                        animator.SetTrigger("Atk1");
                    }
                    if (randomNumber == 2)
                    {
                        animator.SetTrigger("Atk2");
                    }
                    if (randomNumber == 3)
                    {
                        animator.SetTrigger("Atk3");
                    }
                    if (randomNumber == 4)
                    {
                        animator.SetTrigger("AtkSp");
                    }
                    delayAtk = 0f;
                }
                delayAtk += Time.deltaTime;
            }
        }
    }


}
