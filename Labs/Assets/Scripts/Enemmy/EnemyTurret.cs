using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyTurret : Enemy
{
    [SerializeField] float projectileForce;
    [SerializeField] float projectileFireRate;
    [SerializeField] float turretFireDistance;
    
    float timeSinceLastFire;

    public Transform projectileSpawnPointRight;
    public Transform projectileSpawnPointLeft;

    public Projectile projectilePrefab;
    public AudioMixerGroup soundFXGroup;
    PlayerSounds ps;
    public AudioClip fireSound;


    // Start is called before the first frame update
    public override void Start()
    {
        ps = GetComponent<PlayerSounds>();
        
        base.Start();
        if (projectileForce <= 0)
            projectileForce = 7.0f;

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;

        if (turretFireDistance <= 0)
            turretFireDistance = 5.0f;

        if (!projectilePrefab)
        {
            if (verbose)
                Debug.Log("Projectile Prefab has not be set on " + name);
        }
        if (!projectileSpawnPointRight)
        {
            if (verbose)
                Debug.Log("Projectile Spawn Point Right has not be set on " + name);
        }
        if (!projectileSpawnPointLeft)
        {
            if (verbose)
                Debug.Log("Projectile Spawn Point Left has not be set on " + name);
        }
    }

    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!anim.GetBool("Fire"))
        {
            if (GameManager.instance.playerInstance)
            {
                this.sr.flipX = GameManager.instance.playerInstance.transform.position.x > this.transform.position.x;
            }

            float distance = Vector2.Distance(transform.position, GameManager.instance.playerInstance.transform.position);

            if (distance <= turretFireDistance)
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetBool("Fire", true);
                }
            }
        }       
    }

    public void Fire()
    {
        timeSinceLastFire = Time.time;
        ps.Play(fireSound, soundFXGroup);

        if (!sr.flipX)
        {  
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointRight.rotation);
            temp.speed = -projectileForce;
        }
        else 
        { 
             Projectile temp = Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointLeft.rotation);
            temp.speed = projectileForce;
        }
        
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                    if (rb)
                        rb.velocity = Vector2.zero;
        }
            
    }
}
