using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePrefabController : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float velocity2 = 20;
    float realVelocity = -10;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_MORIR = 2;
    const int ANIMATION_ATACAR = 3;
    int vida = 2;
    private Ninja3Controller gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<Ninja3Controller>();
        //Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipX = true;
        rb.velocity = new Vector2(realVelocity, 0);
        if(vida == 0)
        {
            Destroy(this.gameObject);
            gameManager.zombies++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(this.gameObject);//con esto destruye al objeto creado -> con esta condición hacemos que cunado choque contra cualquier objeto se destruya el objeto
        if (collision.gameObject.tag == "Kunai")//aquí le indicamos que cuando colisiona con un enemigo debe hacer...
        {
            vida--;
        }
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
