using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Animator animator;

    private bool isJumping = false;
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
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontal * speed , rb.velocity.y);
            if (horizontal > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (horizontal < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }else
        {
            rb.velocity = new Vector2(horizontal * 0 , rb.velocity.y);
        }
        

    }

    private void UpdateAnimationStates()
    {
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0.1f);

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            animator.SetBool("isAttacking", true);
        }else
        {
            animator.SetBool("isAttacking", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse2)) // Assume Fire2 is Range Attack
        {
            animator.SetBool("isRangeAttacking", true);
        }
        else
        {
            animator.SetBool("isRangeAttacking", false);
        }

    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        isJumping = true; 
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    public void EndDisableMove ()
    {
        Debug.Log("EndDisableMove");
        canMove = true;
    }
    
    public void DisableMove ()
    {
        Debug.Log("DisableMove");
        canMove = false;
    }
}
