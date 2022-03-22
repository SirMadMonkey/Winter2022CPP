using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pickups : MonoBehaviour
{
    PlayerSounds ps;
    public AudioMixerGroup soundFXGroup;

    enum CollectibleType
    {
        POWERUP,
        SCORE,
        LIFE
    }

    [SerializeField] CollectibleType curCollectible;
    public int ScoreValue;
    public AudioClip pickupSound;


    private void Start()
    {
        ps = GetComponent<PlayerSounds>();
        /* if (curCollectible == CollectibleType.LIFE)
         {
             Rigidbody2D rb = GetComponent<Rigidbody2D>();
             rb.velocity = new Vector2(-3, rb.velocity.y);
         }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerSounds ps = collision.gameObject.GetComponent<PlayerSounds>();
            ps.Play(pickupSound, soundFXGroup);
            
            switch (curCollectible)
            {
                case CollectibleType.POWERUP:
                    //collision.gameObject.GetComponent<Player>().StartJumpForceChange();
                    GameManager.instance.score += ScoreValue;
                    break;
               
                case CollectibleType.LIFE:
                    //curPlayerScript.lives++;
                    GameManager.instance.lives++;
                    break;
               
                case CollectibleType.SCORE:
                    GameManager.instance.score += ScoreValue;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
