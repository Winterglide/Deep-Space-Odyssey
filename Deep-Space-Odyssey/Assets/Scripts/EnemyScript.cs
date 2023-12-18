using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Animator Animator;
    public AudioClip HurtSound;
    public Transform character;
    public GameObject EnemyBulletPrefab;

    private int Health = 3;
    private float LastShoot;

    private void start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (character == null) return;

        Vector3 direction = character.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(character.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 1.5f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(transform.localScale.x, 0.0f, 0.0f);
        GameObject enemybullet = Instantiate(EnemyBulletPrefab, transform.position + direction * -0.1f, Quaternion.identity);
        //bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        //Animator.SetBool("hurt", true);
        Health -= 1;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HurtSound);
        if (Health == 0) Destroy(gameObject);
    }

    public void BackToIdle() 
    {
        Animator.SetBool("hurt", false);
    }

}