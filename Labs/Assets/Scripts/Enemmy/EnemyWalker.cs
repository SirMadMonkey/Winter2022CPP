using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{
    Rigidbody2D rb;
    [SerializeField] float speed;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //do my specific setup in here
        

        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 5.0f;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Debug.Log("Enemy Walker took " + damage + " damage");
    }

    void Update()
    {   //& !anim.GetBool("Squish")
        if (!anim.GetBool("Death"))
        {
            if(!sr.flipX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

   /* public void IsSquished()
    {
        anim.SetBool("Squish", true);
        rb.velocity = Vector2.zero;
        Destroy(transform.parent.gameObject, 1f);
    }*/

    public override void Death()
    {
        base.Death();
        anim.SetBool("Death", true);
        rb.velocity = Vector2.zero;
        Destroy(transform.parent.gameObject, 0.5f);
    }
}
