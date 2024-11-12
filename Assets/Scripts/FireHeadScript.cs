using System.Collections;
using System.Collections.Generic;
using ThanhNK;
using UnityEngine;

public class FireHeadScript : MonoBehaviour
{

    private float speed = 5f;
    private bool isMovingRight = true;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (isMovingRight ? 1 : -1) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Animator>().SetTrigger("Hit");
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }
        if (collision.gameObject.CompareTag("EnemyAttack") 
            || collision.gameObject.CompareTag("BloodBottle") 
            || collision.gameObject.CompareTag("ManaBottle"))
        {
            // Ignore collision with other enemies
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            

            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }

    }

    
    public void Die()
    {
        PlayerData.Kills += 1;
        PlayerData.Point += 20;
        Destroy(gameObject);
    }


}
