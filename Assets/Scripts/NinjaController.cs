using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_ATACAR = 3;
    Vector3 lastCheckPointPosition;
    int band = 0;

    //doble salto
    int vecesSalto = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(velocity * 2, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(-velocity * 2, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }
        //else if (Input.GetKeyUp(KeyCode.Space))//salto simple
        //{
        //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //    ChangeAnimation(ANIMATION_SALTAR);
        //}
        else if (Input.GetKeyUp(KeyCode.Space))//salto doble
        {
            if (vecesSalto < 2)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                ChangeAnimation(ANIMATION_SALTAR);
                vecesSalto += 1;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACAR);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        vecesSalto = 0;
        if (collision.gameObject.name == "DarkHole")
        {
            if (lastCheckPointPosition != null)
            {
                transform.position = lastCheckPointPosition;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if(collision.gameObject.name == "Checkpoint2")
        {
            band++;
            lastCheckPointPosition = transform.position;
        }
        if(band <= 0)
        {
            lastCheckPointPosition = transform.position;
        }
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
