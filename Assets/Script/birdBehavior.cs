using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBehavior : MonoBehaviour
{
    private float moveInput;
    public float birdSpeed = 20f;
    private float speed ;
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.Log("rigidBody component failed bird");
        if (transform.position.x > 0) birdSpeed = -birdSpeed;
        

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(birdSpeed*Random.Range(0.5f, 1.1f), rb.velocity.y);
    }

    private void Update()
    {
        if (transform.position.x < -12f || transform.position.x > 12f || transform.position.y < -5.5f)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            Physics2D.IgnoreCollision(other.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            anim.SetTrigger("isDead");
            rb.velocity = new Vector2(birdSpeed, 2);
            rb.gravityScale = 15f;
            rb.position = new Vector3(rb.position.x, rb.position.y, 1f);
            Destroy(this.gameObject, 3f);
        }
    }
}
