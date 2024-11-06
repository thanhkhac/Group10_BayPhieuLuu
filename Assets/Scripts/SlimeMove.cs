using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    [SerializeField] float steerSpeed = 1f;
    
    private Rigidbody2D rb2d;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSkeleton();
    }
    
    private void MoveSkeleton()
    {
        // Cập nhật tốc độ di chuyển dựa trên steerSpeed
        rb2d.velocity = new Vector2(steerSpeed, rb2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Got wall");
            ChangeDirection(); // Đảo ngược hướng di chuyển khi va chạm với tường
        }

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            steerSpeed = 0;
            anim.SetBool("Die", true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    private void ChangeDirection()
    {
        steerSpeed = -steerSpeed; // Đảo ngược tốc độ
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); // Đảo ngược hướng mặt
    }
    
}