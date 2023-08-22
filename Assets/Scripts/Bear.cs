using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bear : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public Vector3 Direction => (target.position - transform.position).normalized;
    private void FixedUpdate()
    {
        rb2D.velocity = Direction * speed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        if(target != null)
        {
            spriteRenderer.flipX = (rb2D.velocity.x < 0);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            GetComponent<Animator>().enabled = false;   
        }
    }
}
