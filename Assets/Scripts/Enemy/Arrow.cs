using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
            Destroy(gameObject);
    }
}
