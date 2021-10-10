using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private float horizontal, vertical;
    public float speed, speedJump;
    public bool absorber;

    public bool inground;
    public int jumps;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        absorber = false;
    }

    // Update is called once per frame
    void Update()
    {
        manageAbsorb();
        if (absorber == false)
		{
            manageMovement();
            manageJump();
            if(inground==false)
			{
                manageFly();
			}
        }
    }

    void manageMovement()
	{
        horizontal = Input.GetAxisRaw("Horizontal");
        
        if(horizontal == 0)
		{
            animator.SetBool("isWalking", false);
		}
		else
		{
            animator.SetBool("isWalking", true);
            if (horizontal > 0)
            {
                rb.velocity = new Vector2(1f * speed, rb.velocity.y);
                animator.SetFloat("ejeX", 1);
            }
            if (horizontal < 0)
            {
                rb.velocity = new Vector2(-1f * speed, rb.velocity.y);
                animator.SetFloat("ejeX", -1);
            }
        }
	}

    void manageJump()
    {
        if ((inground) && Input.GetKeyDown("c"))
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 1f * speedJump);
        }
    }

    void manageAbsorb()
	{
        if (Input.GetKeyDown("x"))
        { 
            print("Absorber");
            absorber = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFlying", false);
            animator.SetBool("isAbsorbing", true);
        }

        if (Input.GetKeyUp("x"))
        {
            print("dejó de Absorber");
            animator.SetBool("isAbsorbing", false);
            absorber = false;           
        }
    }

    void manageFly()//EN EL AIRE
	{
        if (Input.GetKeyDown("c"))
        {
            animator.SetBool("isFlying", true);
            rb.velocity = new Vector2(rb.velocity.x, 1f * speedJump);
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        //DETECTA LA COLISION ENTRE 2 OBJETOS QUE TENGAN COLLIDERS
		if(collision.gameObject.tag=="Ground")
		{
            animator.SetBool("isJumping", false);
            animator.SetBool("isFlying", false);
            inground = true;
        }
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inground = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Enemy")
        {
            print("Enemigo en rango");
        }
    }
}
