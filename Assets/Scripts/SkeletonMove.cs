using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMove : MonoBehaviour
{
    [SerializeField] float steerSpeed = 2f;
    private Animator anim;
    private Transform playerTransform;
    private float stopDistance = 2.5f;
    private float followDistance = 4f;
    private Rigidbody2D rb2d;
    private BoxCollider2D[] boxCollider;

    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponentsInChildren<BoxCollider2D>();
        boxCollider = System.Array.FindAll(boxCollider, col => col.gameObject != this.gameObject);
        
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // if (player != null)
        // {
        //     playerTransform = player.transform;
        // }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("11111111111111111111111111111111111111111111111111111111111111111111111111111111");
            playerTransform = player.transform;
            Debug.Log("player: "+ playerTransform.name);
        }
        else
        {
            Debug.LogWarning("No Player object found with the tag 'Player'.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit) // Kiểm tra nếu chưa bị đánh thì tiếp tục di chuyển
        {
            Debug.Log("ishit false");
            distanceToPlayer(); // Kiểm tra khoảng cách tới player
            MoveSkeleton(); // Di chuyển kẻ thù
        }
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
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Got player");
            StartCoroutine(StopAndPlayHitAnimation());
        }
    }

    private IEnumerator StopAndPlayHitAnimation()
    {
        isHit = true;  // Đặt isHit thành true để ngừng di chuyển
        steerSpeed = 0;  // Ngừng di chuyển ngay lập tức
        rb2d.velocity = Vector2.zero;  // Đặt vận tốc về 0 để chắc chắn dừng lại

        yield return new WaitForSeconds(0.1f);  // Đợi một chút trước khi bật hoạt ảnh "Hit"

        anim.SetBool("Hit", true);  // Chạy hoạt ảnh "Hit"
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        
        EnableChildBoxColliders(false);  // Tắt collider của đối tượng con nếu cần

    }
    
    public void EnableChildBoxColliders(bool enable)
    {
        // Bật/tắt tất cả các BoxCollider2D
        foreach (BoxCollider2D col in boxCollider)
        {
            col.enabled = enable;
        }
    }

    public void DisableAnim()
    {
        anim.enabled = false;
    }

    private void ChangeDirection()
    {
        steerSpeed = -steerSpeed; // Đảo ngược tốc độ
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); // Đảo ngược hướng mặt
    }

    public void distanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log("2222222222222222  " + distance);
        
        if (distance < stopDistance)
        {
            steerSpeed = 0; // Ngừng di chuyển
            anim.SetBool("Attack", true);
            Debug.Log("1    " + distance);
        }
        else if (distance <= followDistance)
        {
            Debug.Log("3    " + distance);
            anim.SetBool("Attack", false);
            // Di chuyển về phía player
            float directionToPlayer = playerTransform.position.x - transform.position.x;
            steerSpeed = directionToPlayer > 0 ? 1f : -1f; // Đặt tốc độ theo hướng tới player

            // Quay mặt kẻ thù về phía player
            if ((directionToPlayer < 0 && transform.localScale.x > 0) ||
                (directionToPlayer > 0 && transform.localScale.x < 0))
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }
    }
}