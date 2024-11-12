using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ThanhNK;
using UnityEngine;
using UnityEngine.UI;

public class BossDemonScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float attackCooldown = 5f;
    [SerializeField] float specialAttackcooldown = 15f;

    [SerializeField] GameObject nightmareSummonPoint;
    [SerializeField] GameObject ghostSummonPoint;
    [SerializeField] GameObject sealBossRoom;
    [SerializeField] NightMareScript nightMare;
    [SerializeField] GhostScript ghost;

    [SerializeField] ItemControl manaBottle;
    [SerializeField] ItemControl bloodBottle;

    [SerializeField] AudioClip SpecialAttackSound;
    [SerializeField] AudioClip AttackSound;

    public float moveSpeed = 1f; // Tốc độ di chuyển
    public float stopDistance = 1f; // Khoảng cách dừng lại

    private Transform playerTransform; // Biến để lưu Transform của Player
    private AudioSource audioSource;

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

    public Transform attackPoint;
    public LayerMask enemyLayer;

    public int attackDamage = 1;
    public float attackRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        fireRangeL = transform.position.x - 5;
        fireRangeR = transform.position.x + 5;
        BossCurrentHealth = BossHealth;

        audioSource = GetComponent<AudioSource>();

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

        if ((playerX >= fireRangeL - 5 && playerX <= fireRangeR + 10))
        {
            gameObject.SetActive(true);
        }

        canSpecialAttack = (playerX >= fireRangeL - 10 && playerX <= fireRangeR + 10) && (specialAttackcooldown <= specialTimerCoolDown);

        canAttack = (playerX >= fireRangeL - 5 && playerX <= fireRangeR + 10);

        attackTimerCoolDown += Time.deltaTime;
        specialTimerCoolDown += Time.deltaTime;

        // Call attack methods based on conditions
        if (canSpecialAttack)
        {
            SpecialAttack();
        }
        if (canAttack)
        {
            Attack();
        }
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
        gameObject.GetComponent<Animator>().SetTrigger("SpecialAttack");

        audioSource.PlayOneShot(SpecialAttackSound);

        specialTimerCoolDown = 0;
        attackTimerCoolDown = 0;
    }

    void Attack()
    {
        if (attackCooldown <= attackTimerCoolDown)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D e in hitEnemies)
            {
                Debug.Log("Hit: " + e.name);
            }

            audioSource.PlayOneShot(AttackSound);
            attackTimerCoolDown = 0;

            SummonGhost();
        }
    }
    public void SummonNightMare()
    {
        var newNightMare = Instantiate(nightMare);
        newNightMare.transform.position = nightmareSummonPoint.transform.position;
        newNightMare.SetDirection(transform.localScale.x);

        var newManaBottle = Instantiate(manaBottle);
        var newBloodBottle = Instantiate(manaBottle);

        newManaBottle.transform.position = ghostSummonPoint.transform.position;
        newBloodBottle.transform.position = ghostSummonPoint.transform.position;

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
        PlayerData.Kills += 1;
        PlayerData.Point += 100;
        Destroy(gameObject);
        sealBossRoom.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            BossCurrentHealth -= 10;
            HealthImg.fillAmount = BossCurrentHealth / 500f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
