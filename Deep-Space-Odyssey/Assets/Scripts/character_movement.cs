using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour
{
    public AudioClip DeathSound;
    public AudioClip HurtSound;
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;

    // Start is called before the first frame update
    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);




        Debug.DrawRay(transform.position, Vector3.down * 0.15f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.15f))
        {
            Grounded = true;
        } else {
            Grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.E) && Time.time > LastShoot + 0.25f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Shoot();
            LastShoot = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > LastShoot + 0.25f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            Shoot();
            LastShoot = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Animator.SetBool("shooting", false);
            Animator.SetBool("shootingbehind", false);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Animator.SetBool("shooting", false);
            Animator.SetBool("shootingbehind", false);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Animator.SetBool("shooting", true);
        Vector3 direction;
        
        if (Input.GetKeyDown(KeyCode.E) && Time.time > LastShoot + 0.25f)
        {
            Animator.SetBool("shootingbehind", Horizontal < 0.0f);
            direction = Vector3.right;
            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);

        }
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > LastShoot + 0.25f)
        {
            Animator.SetBool("shootingbehind", Horizontal > 0.0f);
            direction = Vector3.left;
            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);

        } 

        //bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Animator.SetBool("hurt", true);
        Health -= 1;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HurtSound);
        if (Health == 0) {
            Destroy(gameObject);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(DeathSound);
        }
        
    }

    public void BackToIdle() 
    {
        Animator.SetBool("hurt", false);
    }


}
