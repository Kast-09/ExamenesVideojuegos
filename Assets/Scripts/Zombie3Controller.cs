using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie3Controller : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5, velocityEscalar = 0.5f, velocityPlanear = 2;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    AudioSource audioSource;
    BoxCollider2D boxCollider;

    int vidas = 3;

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
        if(vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPequenia") vidas--;
        if (collision.gameObject.tag == "BalaMediana") vidas -= 2;
        if (collision.gameObject.tag == "BalaGrande") vidas -= 3;
    }
}
