using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja3Controller : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5, velocityEscalar = 0.5f, velocityPlanear = 2;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    AudioSource audioSource;
    BoxCollider2D boxCollider;

    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_ATACAR = 3;
    const int ANIMATION_ESCALAR = 4;
    const int ANIMATION_DESLIZAR = 5;
    const int ANIMATION_PLANEAR = 6;

    Vector3 lastCheckPointPosition;
    public GameObject bullet;
    public GameObject Suelo2;

    public AudioClip jumpClip;//variables para almacenar cada audio
    public AudioClip bulletClip;
    public AudioClip crecerClip;

    //doble salto
    int vecesSalto = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
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
        else if (Input.GetKeyUp(KeyCode.Space))//salto doble
        {
            if (vecesSalto < 2)
            {
                audioSource.PlayOneShot(jumpClip);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                ChangeAnimation(ANIMATION_SALTAR);
                vecesSalto += 1;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            audioSource.PlayOneShot(bulletClip);
            ChangeAnimation(ANIMATION_ATACAR);
            if (sr.flipX == false)
            {
                //crear bala
                var bulletPosition = transform.position + new Vector3(3, 0, 0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BulletController>();
                controller.SetRightDirection();
            }

            if (sr.flipX == true)
            {
                //crear bala
                var bulletPosition = transform.position + new Vector3(-3, 0, 0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BulletController>();
                controller.SetLeftDirection();
            }
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.gravityScale = 10;
            if (sr.flipX == true)
                rb.velocity = new Vector2(-velocity, rb.velocity.y);
            else
                rb.velocity = new Vector2(velocity, rb.velocity.y);
            ChangeAnimation(ANIMATION_DESLIZAR);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && boxCollider.IsTouchingLayers(LayerMask.GetMask("Escalera")))
        {
            rb.gravityScale = 0;
            ChangeAnimation(ANIMATION_ESCALAR);
            rb.velocity = new Vector2(0, velocity);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && boxCollider.IsTouchingLayers(LayerMask.GetMask("Escalera")))
        {
            rb.gravityScale = 0;
            ChangeAnimation(ANIMATION_ESCALAR);
            rb.velocity = new Vector2(0, -velocityEscalar);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ChangeAnimation(ANIMATION_PLANEAR);
            rb.velocity = new Vector2(rb.velocity.x, -velocityPlanear);
        }
        else
        {
            if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Escalera")))
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, 0);
                ChangeAnimation(ANIMATION_ESCALAR);
                Suelo2.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                rb.gravityScale = 1;
                rb.velocity = new Vector2(0, rb.velocity.y);
                //Suelo2.GetComponent<Collider>().enabled = true;
                ChangeAnimation(ANIMATION_QUIETO);
                Suelo2.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        vecesSalto = 0;
        if (collision.gameObject.tag == "Enemy")//con esto detecto si la clasificai?n del objeto es un enemigo y en base a la condici?n determina si mueres o no
        {
            Debug.Log("Estas Muerto");
        }
        if (collision.gameObject.name == "DarkHole")
        {
            if (lastCheckPointPosition != null)
            {
                transform.position = lastCheckPointPosition;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        lastCheckPointPosition = transform.position;
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
