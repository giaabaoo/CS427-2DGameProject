using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    public GameObject hitEffect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); 

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    //public GameObject hitEffect;

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //    Destroy(effect, 5f);
    //    Destroy(gameObject);
    //}
    
}
