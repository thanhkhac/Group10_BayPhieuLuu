using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DemonSpawnPointScript : MonoBehaviour
{
    [SerializeField] GameObject DemonBoss;
    [SerializeField] GameObject DemonBoss_RoomSeal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DemonBoss.SetActive(true);
            DemonBoss_RoomSeal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
