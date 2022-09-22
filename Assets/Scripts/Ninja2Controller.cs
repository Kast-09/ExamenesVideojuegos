using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja2Controller : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5, velocityEscalar = 5, velocityPlanear = 2;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    private GameManagerController gameManager;

    public GameObject bullet;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_ATACAR = 3;
    const int ANIMATION_ESCALAR = 4;
    const int ANIMATION_DESLIZAR = 5;
    const int ANIMATION_PLANEAR = 6;

    public AudioClip jumpClip;//variables para almacenar cada audio
    public AudioClip coinClip;

    Vector3 lastCheckPointPosition;

    AudioSource audioSource;
    //doble salto
    int vecesSalto = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManagerController>();
        float X = gameManager.PositionX();
        float Y = gameManager.PositionY();
        float Z = gameManager.PositionZ();
        if (X != 0.0f || Y != 0.0f || Z != 0.0f)
        {
            transform.position = new Vector3(X, Y, Z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
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
            if (sr.flipX == true)
            {
                var bulletPosition = transform.position + new Vector3(-3, 0, 0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity);
                var controller = gb.GetComponent<BulletController>();
                controller.SetLeftDirection();
            }
            else
            {
                var bulletPosition = transform.position + new Vector3(3, 0, 0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity);
                var controller = gb.GetComponent<BulletController>();
                controller.SetRightDirection();
            }
            ChangeAnimation(ANIMATION_ATACAR);
        }
        else
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        vecesSalto = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lastCheckPointPosition = transform.position;
        if (collision.gameObject.tag == "Guardar")
        {
            gameManager.SetLastCheckPosition(lastCheckPointPosition.x, 
                                            lastCheckPointPosition.y, 
                                            lastCheckPointPosition.z);
            gameManager.SaveGame();
        }
        if (collision.gameObject.tag == "MonedaBronze")
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinClip);
            gameManager.SumarBronze();
        }
        if (collision.gameObject.tag == "MonedaPlata")
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinClip);
            gameManager.SumarPlata();
        }
        if (collision.gameObject.tag == "MonedaOro")
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinClip);
            gameManager.SumarOro();
        }
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
