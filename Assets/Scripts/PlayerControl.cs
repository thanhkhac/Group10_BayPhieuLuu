using System;
using ThanhNK;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public Animator animator;

    private bool isJumping = false;
    public bool isImmortal = false;
    private bool canMove = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimationStates();
        horizontal = Input.GetAxis("Horizontal");
        Move();

        if (Input.GetButtonDown("Jump") && IsGrounded()) { Jump(); }
        animator.SetBool("IsJumping", !IsGrounded());
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (horizontal > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (horizontal < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else { rb.velocity = new Vector2(horizontal * 0, rb.velocity.y); }
    }

    private void UpdateAnimationStates()
    {
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0.1f);

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Chuột trái
        {
            animator.SetBool("isAttacking", true);
        }
        else { animator.SetBool("isAttacking", false); }

        if (Input.GetKeyDown(KeyCode.Mouse2)) //Chuột giữa
        {
            animator.SetBool("isRangeAttacking", true);
        }
        else { animator.SetBool("isRangeAttacking", false); }
        
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            animator.SetBool("isUltimate", true);
        }
        else { animator.SetBool("isUltimate", false); }
        
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            animator.SetBool("isDefending", true);
        }
        else { animator.SetBool("isDefending", false); }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        isJumping = true;
        animator.SetBool("IsJumping", true);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    public void EndDisableMove()
    {
        Debug.Log("EndDisableMove");
        canMove = true;
    }

    public void DisableMove()
    {
        Debug.Log("DisableMove");
        canMove = false;
    }
    
    public void EnableImmortal()
    {
        Debug.Log("Enable Immortal");
        isImmortal = true;
    }
    
    public void DisableImmortal()
    {
        Debug.Log("Disable Immortal");
        isImmortal = false;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("EnemyAttack") && !isImmortal)
    //     {
    //         animator.SetTrigger("Hit");
    //         GameManager.PLayerHealth -=10;
    //         Debug.Log(GameManager.PLayerHealth);
    //     }
    // }
}