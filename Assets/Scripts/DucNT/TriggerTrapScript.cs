using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTrapSript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject Trap;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Player"))
        {
            //trigger trap
            Trap.GetComponent<Animator>().SetTrigger("Active");
        }

    }


}
