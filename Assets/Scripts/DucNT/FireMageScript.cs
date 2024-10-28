using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;
    [SerializeField] float attackcooldown = 5f;
    [SerializeField] GameObject firePoint;
    [SerializeField] FireBallScript fireBall;

    float timerCoolDown;
    private bool isFacingRight = true;
    private bool canFire = false;
    private float fireRangeL;
    private float fireRangeR;
    void Start()
    {
        fireRangeL = transform.position.x - 10;
        fireRangeR = transform.position.x + 10;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        timerCoolDown += Time.deltaTime;
        Attack();

        canFire = player.transform.position.x >= fireRangeL && player.transform.position.x <= fireRangeR;
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

    void Attack()
    {
        if (canFire)
        {
            if (attackcooldown <= timerCoolDown)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
                timerCoolDown = 0;
            }
        }
    }

    public void Fire()
    {
        var newfireBall = Instantiate(fireBall);
        newfireBall.transform.position = firePoint.transform.position;
        newfireBall.SetDirection(-transform.localScale.x);
    }
}
