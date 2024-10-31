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

    float timerCoolDown;
    float specialTimerCoolDown;
    private bool isFacingRight = true;

    private bool canSpecialAttack = false;
    private bool canAttack = false;

    private float fireRangeL;
    private float fireRangeR;

    // Start is called before the first frame update
    void Start()
    {
        fireRangeL = transform.position.x - 5;
        fireRangeR = transform.position.x + 5;
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
