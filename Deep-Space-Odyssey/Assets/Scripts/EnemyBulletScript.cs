using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_script : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;

    // Start is called before the first frame update
    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();     
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);

        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);  
        Horizontal = -1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyScript flower = other.GetComponent<EnemyScript>();
        character_movement character = other.GetComponent<character_movement>();
        if (flower != null)
        {
            flower.Hit();
        }
        if (character != null)
        {
            character.Hit();
        }
        DestroyBullet();
    }
}

