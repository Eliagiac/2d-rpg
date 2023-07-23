using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHandler : MonoBehaviour
{
    public bool isLaunching;

    public Transform target;

    public float speed = 1f;

    public int dmg = 10;

    Rigidbody2D rb;

    Vector2 movement;

    Vector2 direction;

    bool hasLaunched = true;

    bool check;

    private void Start()
    {
        target.GetComponent<PlayerHandler>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isLaunching)
        {
            gameObject.tag = "Weapon";

            if (hasLaunched)
            {
                direction = new Vector3(target.position.x, target.position.y + 1.065028f, target.position.z) - transform.position;

                movement = direction / direction.magnitude;
            }

            hasLaunched = false;

            LaunchRock(target);
        }

        if (GetComponent<ParticleSystem>().isPlaying)
        {
            check = true;
        }

        else if (check)
        {
            Destroy(gameObject);
        }
    }

    public void LaunchRock(Transform target)
    {
        rb.MovePosition(rb.position + movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isLaunching)
        {
            PlayerHandler.DamagePlayer(dmg);

            GetComponent<ParticleSystem>().Play();

            GetComponent<SpriteRenderer>().enabled = false;

            isLaunching = false;
        }

        else if (collision.gameObject.CompareTag("Tree") && isLaunching)
        {
            GetComponent<ParticleSystem>().Play();
        
            GetComponent<SpriteRenderer>().enabled = false;

            isLaunching = false;
        }
    }
}