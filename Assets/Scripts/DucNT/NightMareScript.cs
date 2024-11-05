using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMareScript : MonoBehaviour
{
    [SerializeField] float speed;
    private GameObject player;
    private float direction;

    private float initiatedPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        initiatedPoint = transform.position.x;

        direction = direction != 0 ? direction : 1; // Default direction to 1 if not set
        transform.localScale = new Vector3(Mathf.Sign(direction), transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        float movement = speed * Time.deltaTime * (-direction); //move
        transform.Translate(movement, 0, 0);
        if (transform.position.x <= initiatedPoint - 50  || transform.position.x >= initiatedPoint + 50) Destroy(gameObject);
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player") )
        {
            //player hurt
            if (player == null) return;

            player.GetComponent<Animator>().SetTrigger("Hit");
            //Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ThanhNK_Ground"))
        {
            Destroy(gameObject);
        }
    }
}
