using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Player))]
public class PlayerFire : MonoBehaviour
{
    public bool verbose = false;

    SpriteRenderer sr;
    Animator anim;
    PlayerSounds ps;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    
    public AudioClip fireSound;
    public AudioMixerGroup soundFXGroup;

    public float projectileSpeed;
    public Projectile projectilePrefab;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerSounds>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            if (verbose)
                Debug.Log("Inspector Values Not Set");

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("fire");
            //Debug.Log("E pressed");
            ps.Play(fireSound, soundFXGroup);
        }
    }

    public void FireProjectile()
    {

        if (sr.flipX)
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = projectileSpeed;
        }   
        else
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            temp.speed = -projectileSpeed;        
        }
    }       
}
