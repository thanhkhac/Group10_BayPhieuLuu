using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSrc : MonoBehaviour
{
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject point;

    public GameObject angelFinalPoint;


    private Transform playerTransform;
    private float distanceToSpawn = 7f;
    private float spawnCooldown = 2f;
    private bool canSpawn = true;
    private Animator anim;

    private float timerSpawnCooldown;
    public Blood blood;
    float currentBlood;
    float maxBlood = 100;
    void Start()
    {
        currentBlood = maxBlood;
        blood.UpdateBlood(currentBlood, maxBlood);

        anim = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("No Player object found with the tag 'Player'.");
        }
    }

    void Update()
    {
        // distanceToPlayer();
        // timerSpawnCooldown += Time.deltaTime;
        //
        // if (timerSpawnCooldown >= spawnCooldown)
        // {
        //     canSpawn = true;
        // }

        distanceToPlayer();

        // Đếm thời gian và kiểm tra nếu đã qua thời gian spawnCooldown
        if (!canSpawn)
        {
            timerSpawnCooldown += Time.deltaTime;
            if (timerSpawnCooldown >= spawnCooldown)
            {
                canSpawn = true;
                timerSpawnCooldown = 0f; // Đặt lại timer sau khi cooldown hoàn thành
            }
        }
    }

    public void distanceToPlayer()
    {
        // float distance = Vector3.Distance(transform.position, playerTransform.position);
        //
        // if (distance < distanceToSpawn && canSpawn)
        // {
        //     //summonSkeleton();
        //     anim.SetTrigger("Summon");
        // }
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < distanceToSpawn && canSpawn)
        {
            // Thực hiện hành động summon
            anim.SetTrigger("Summon");

            // Đặt lại điều kiện spawn và bắt đầu lại timer
            canSpawn = false;
            timerSpawnCooldown = 0f;
        }
    }

    public void SummonSkeleton()
    {
        canSpawn = false;
        // Tạo bản sao skeleton nhưng chưa kích hoạt
        var newSkeleton = Instantiate(skeleton, point.transform.position, Quaternion.identity);
        timerSpawnCooldown = 0f;
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "PlayerAttack")
        {
            Debug.Log("player attack boss lv1");
            currentBlood -= 10;
            blood.UpdateBlood(currentBlood, maxBlood);
            if (currentBlood <= 0)
            {
                anim.SetTrigger("Die");
            }

        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
        if (angelFinalPoint != null)
        {
            var finalPoint = Instantiate(angelFinalPoint);
            finalPoint.transform.position = transform.position;
        }
    }
}