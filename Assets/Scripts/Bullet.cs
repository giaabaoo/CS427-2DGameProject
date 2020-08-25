using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    public GameObject hitEffect;

    public float thrust;
    public float knockTime;
    public float damage = 1;

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

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.gameObject.CompareTag("breakable")) {
            hitInfo.GetComponent<Pot>().Smash();
        }

        if (hitInfo.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = hitInfo.GetComponent<Rigidbody2D>();
            

            if (hit != null) {
                
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (hitInfo.gameObject.CompareTag("Player"))
                {
                    if (hitInfo.GetComponent<PlayerController>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerController>().currentState = PlayerState.stagger;
                        hitInfo.GetComponent<PlayerController>().Knock(knockTime, damage);
                    }
                }
                
                
            }
        }


    }

    //public GameObject hitEffect;

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //    Destroy(effect, 5f);
    //    Destroy(gameObject);
    //}
    
}
