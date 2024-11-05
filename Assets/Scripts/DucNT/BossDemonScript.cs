using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDemonScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float attackCooldown = 5f;
    [SerializeField] float specialAttackcooldown = 15f;

    [SerializeField] GameObject nightmareSummonPoint;
    [SerializeField] GameObject ghostSummonPoint;
    [SerializeField] NightMareScript nightMare;
    [SerializeField] GhostScript ghost;

    public float moveSpeed = 1f; // Tốc độ di chuyển
    public float stopDistance = 1f; // Khoảng cách dừng lại

    private Transform playerTransform; // Biến để lưu Transform của Player

    float attackTimerCoolDown;
    float specialTimerCoolDown;
    private bool isFacingRight = true;

    private bool canSpecialAttack = false;
    private bool canAttack = false;

    private float fireRangeL;
    private float fireRangeR;
    private float delayTime = 5f;

    public float BossHealth = 500f;
    public float BossCurrentHealth = 500f;
    public Image HealthImg;

    // Start is called before the first frame update
    void Start()
    {
        fireRangeL = transform.position.x - 5;
        fireRangeR = transform.position.x + 5;
        BossCurrentHealth = BossHealth;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform; // Lưu Transform của Player
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BossCurrentHealth <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }

        UpdateDirection();

        fireRangeL = transform.position.x;
        fireRangeR = transform.position.x;

        var playerX = player.transform.position.x;
        canSpecialAttack = (playerX >= fireRangeL - 10 && playerX <= fireRangeR + 10) && (specialAttackcooldown <= specialTimerCoolDown);

        canAttack = (playerX >= fireRangeL && playerX <= fireRangeR);

        attackTimerCoolDown += Time.deltaTime;
        specialTimerCoolDown += Time.deltaTime;

        // Call attack methods based on conditions
        if (canSpecialAttack)
        {
            SpecialAttack();
        }

        Attack();

        // Check player in range only once per frame

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
        BossCurrentHealth -= 150f;
        HealthImg.fillAmount = BossCurrentHealth / BossHealth;

        gameObject.GetComponent<Animator>().SetTrigger("SpecialAttack");
        specialTimerCoolDown = 0;
        attackTimerCoolDown = 0;
    }


    void Attack()
    {
        if (attackCooldown <= attackTimerCoolDown)
        {
            BossCurrentHealth -= 100f;
            HealthImg.fillAmount = BossCurrentHealth / BossHealth;

            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            attackTimerCoolDown = 0;
            SummonGhost();
        }
    }
    public void SummonNightMare()
    {
        var newNightMare = Instantiate(nightMare);
        newNightMare.transform.position = nightmareSummonPoint.transform.position;
        newNightMare.SetDirection(transform.localScale.x);
        SummonGhost();
    }

    void SummonGhost()
    {
        var newGhost = Instantiate(ghost);
        newGhost.transform.position = ghostSummonPoint.transform.position;
        newGhost.SetDirection(new Vector2(-transform.localScale.x, transform.localScale.y));
        newGhost.gameObject.SetActive(true);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
