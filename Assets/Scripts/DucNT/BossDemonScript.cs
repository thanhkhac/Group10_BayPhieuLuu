using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDemonScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float attackcooldown = 5f;
    [SerializeField] float specialAttackcooldown = 15f;

    [SerializeField] GameObject summonPoint;
    [SerializeField] NightMareScript nightMare;

    public float moveSpeed = 1f; // Tốc độ di chuyển
    public float stopDistance = 1f; // Khoảng cách dừng lại

    private Transform playerTransform; // Biến để lưu Transform của Player

    float timerCoolDown;
    float specialTimerCoolDown;
    private bool isFacingRight = true;

    private bool canSpecialAttack = false;
    private bool canAttack = false;

    private float fireRangeL;
    private float fireRangeR;
    private float delayTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        fireRangeL = transform.position.x - 5;
        fireRangeR = transform.position.x + 5;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform; // Lưu Transform của Player
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();

        timerCoolDown += Time.deltaTime;
        specialTimerCoolDown += Time.deltaTime;

        // Call attack methods based on conditions
        if (canSpecialAttack)
            SpecialAttack();
        else if (canAttack)
            Attack();

        // Check player in range only once per frame
        canAttack = canSpecialAttack = player.transform.position.x >= fireRangeL && player.transform.position.x <= fireRangeR;

        BossMovement();
    }

    void UpdateDirection()
    {
        var scaler = gameObject.transform.localScale;

        var playerDirection = player.transform.position.x - transform.position.x;

        if (playerDirection > 0 && !isFacingRight)
        {
            isFacingRight = true;
            scaler.x = -Math.Abs(scaler.x);
        }
        else if (playerDirection < 0 && isFacingRight)
        {
            isFacingRight = false;
            scaler.x = Math.Abs(scaler.x);
        }
        transform.localScale = scaler;
    }

    void BossMovement()
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
            // Kiểm tra khoảng cách đến Player

            MoveWithPlayer(distanceToPlayer, direction, relativePosition);

            //if (BossHealth >= 0)
            //{
            //    Acttack(distanceToPlayer);
            //}
            //checkHeathBoss();
        }
    }
    public void MoveWithPlayer(float distanceToPlayer, Vector3 direction, Vector3 relativePosition)
    {
        if (distanceToPlayer > stopDistance)
        {
            // Di chuyển theo hướng của Player
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void SpecialAttack()
    {
        if (specialAttackcooldown <= specialTimerCoolDown)
        {
            gameObject.GetComponent<Animator>().SetTrigger("SpecialAttack");
            specialTimerCoolDown = 0;
        }
    }

    void Attack()
    {
        if (attackcooldown <= timerCoolDown)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            timerCoolDown = 0;
        }
    }

    public void SummonNightMare()
    {
        var newNightMare = Instantiate(nightMare);
        newNightMare.transform.position = summonPoint.transform.position;
        newNightMare.SetDirection(transform.localScale.x);
    }
}
