using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie2Controller : MonoBehaviour
{
    public float velocity = 1.5f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D boxCollider;
    int band = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (band == 0)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = true;
        }
        if (band == 1)
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Limite1")
        {
            band = 1;
        }
        if (collision.gameObject.tag == "Limite2")
        {
            band = 0;
        }
    }
}
