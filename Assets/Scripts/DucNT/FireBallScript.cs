using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
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

    }

    // Update is called once per frame
    void Update()
    {
        float movement = speed * Time.deltaTime * direction; //move
        transform.Translate(movement, 0, 0);

        if (transform.position.x <= initiatedPoint - 20 || transform.position.x >= initiatedPoint + 20) Destroy(gameObject);
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //player hurt
            if (player == null) return;

            player.GetComponent<Animator>().SetTrigger("Hit");
            Destroy(gameObject);
        }
    }

}
