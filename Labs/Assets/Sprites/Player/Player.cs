using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    public bool verbose = false;
    public bool isGrounded;
    public bool Att;
    
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    
    //[SerializeField]
    public float speed;
    public int jumpforce;
    public float groundCheckRadius;
    public LayerMask isGroundLayer;
    public Transform groundCheck;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

       

        if (speed <= 0)
        {
            speed = 5.0f;
            if (verbose)
                Debug.Log("Speed changed to default 5");
        }

        if (jumpforce <= 0)
        {
            jumpforce = 300;
            if (verbose)
                Debug.Log("Jump Force changed to default 300");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.05f;
            if (verbose)
                Debug.Log("Ground Check Radius changed to default 0.05");
        }

        if (!groundCheck)
        {
            groundCheck = transform.GetChild(0);
            if (verbose)
            {
                if (groundCheck.name == "GroundCheck")
                    Debug.Log("Ground Check Found and assigned");
                else
                    Debug.Log("Manually set ground check");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        //jump animation
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpforce);
        }

        Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDir;
        
        //attack animation
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Att", true);
            Debug.Log("CTRL was pressed");

        }

        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Att", false);
            Debug.Log("CTRL was released");
        }

        //Crouch attack
        if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire3"))
        {
            //anim.SetTrigger("crouch_Att");
            anim.SetBool("Crouch", true);
            anim.SetBool("Att", true);
        }

        //Crouch anim
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetBool("Crouch", true);
            Debug.Log("SHIFT was pressed");
        }

        if (Input.GetButtonUp("Fire3"))
        {
            anim.SetBool("Crouch", false);
            Debug.Log("Shift was released");
        }

        anim.SetFloat("xVel", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);;

        if (hInput < 0 && sr.flipX || hInput > 0 && !sr.flipX)
            sr.flipX = !sr.flipX;

        
    }
}
