using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float velocity = 3.5f, jumpForce = 20;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_MORIR = 3;
    int band = 0;
    public GameObject bullet;
    bool estaMuerto = false;
    int contBalas = 0;
    private GameManagerController gameManager;

    //doble salto
    int vecesSalto = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        ChangeAnimation(ANIMATION_CORRER);
        if (estaMuerto == true)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_MORIR);
        } else if (Input.GetKeyUp(KeyCode.Space))//salto doble
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            ChangeAnimation(ANIMATION_SALTAR);
        }
        else if (Input.GetKeyUp(KeyCode.Z) && contBalas < 5)
        {
            //crear bala
            gameManager.RestarBalas();
            var bulletPosition = transform.position + new Vector3(3, 0, 0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
            contBalas++;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            estaMuerto = true;            
        }
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
