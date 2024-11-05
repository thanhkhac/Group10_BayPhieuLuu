using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public float speed;
    public GameObject target;
    private Vector2 direction;

    public float health = 15f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > health)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }
    }

    private void FixedUpdate()
    {
        System.Random rnd = new System.Random();
        var randomSpeed = rnd.Next(3, int.Parse((speed + 1).ToString())); // The second parameter is exclusive, so add 1 to include `speed`

        Vector2 currentPosition = transform.position;

        Vector2 playerPosition = target.transform.position;

        Vector2 direction = (playerPosition - currentPosition).normalized;

        // Move the game object toward the player
        transform.position = Vector2.MoveTowards(currentPosition, target.transform.position, randomSpeed * Time.deltaTime);
        transform.up = target.transform.position - transform.position;
        // Pass the direction to the rotation method
        RotateTowardDirection(direction);
        //Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
        //Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner
    }


    void RotateTowardDirection(Vector2 direction)
    {
        // Calculate the angle between the object's direction and the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Check if the player is to the left of the object
        if (direction.x < 0)
        {
            // If the player is on the left, flip the angle
            angle -= 180;

            // Flip the object's scale along the X-axis
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Reset the scale if the player is on the right
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }

        // Apply the rotation on the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void SetDirection(Vector2 _direction)
    {
        //set normalized to get unit vector
        direction = _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //player hit
            //sound
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
