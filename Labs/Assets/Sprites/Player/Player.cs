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
    
    [SerializeField]
    public float speed;
    
    [SerializeField]
    public int jumpforce;
    
    [SerializeField]
    public float groundCheckRadius;
    
    [SerializeField]
    public LayerMask isGroundLayer;
    
    [SerializeField]
    public Transform groundCheck;

    bool coroutineRunning = false;


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
        AnimatorClipInfo[] curState = anim.GetCurrentAnimatorClipInfo(0); 
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
            /*Debug.Log("CTRL was pressed");*/

        }

        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Att", false);
           /* Debug.Log("CTRL was released");*/
        }

        
        anim.SetFloat("xVel", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);;

        if (hInput < 0 && sr.flipX || hInput > 0 && !sr.flipX)
            sr.flipX = !sr.flipX;


        //Crouch attack
        if (Input.GetButton("Fire3"))
        {

            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetBool("crouchAtt", true);
                //anim.SetBool("Att", true);
            }

            if (curState[0].clip.name == "Crouch")
                return;

            anim.SetBool("Crouch", true);

        }
        else
        {
            anim.SetBool("Crouch", false);
        }

        if (Input.GetButton("Fire3"))
        {

            if (Input.GetButtonUp("Fire1"))
            {
                anim.SetBool("crouchAtt", false);
                //anim.SetBool("Att", true);
            }

            if (curState[0].clip.name == "Crouch")
                return;

            anim.SetBool("Crouch", true);

        }
        else
        {
            anim.SetBool("Crouch", false);
        }
    }

    public void StartJumpForceChange()
    {
        if (!coroutineRunning)
            StartCoroutine("JumpForceChange");
        else
        {
            StopCoroutine("JumpForceChange");
            jumpforce /= 2;
            StartCoroutine("JumpForceChange");
        }
    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        jumpforce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpforce /= 2;
        coroutineRunning = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Squish")
        {
            col.gameObject.GetComponentInParent<EnemyWalker>().Death();
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 1);
            Destroy(col.gameObject);

        }
    }
}
