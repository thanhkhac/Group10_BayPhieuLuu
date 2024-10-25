using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapParallax : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
    }
}
