using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damageValue;
    

    // Start is called before the first frame update
    void Start()
    {
        
        if (lifetime <= 0)
            lifetime = 2.0f;

        if (damageValue <= 0)
            damageValue = 2;
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "PlayerProjectile")
            {
                Enemy e = collision.gameObject.GetComponent<Enemy>();

                if (e)
                    e.TakeDamage(damageValue);

                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (rb)
                    rb.velocity = Vector2.zero;

                Destroy(gameObject);
            }
        }


        if (collision.gameObject.tag == "Player" && gameObject.tag == "enemyProjectile")
        {
            GameManager.instance.lives--;
            Destroy(gameObject);
        }
    }
}