using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;

    public int damage_boss_bow = 2;

    // void OnTriggerEnter2D (Collider2D hitInfo)
    // {
    // 	Enemy enemy = hitInfo.GetComponent<Enemy>();
    // 	if (enemy != null)
    // 	{
    // 		enemy.TakeDamage(damage);
    // 	}

    // 	Instantiate(impactEffect, transform.position, transform.rotation);

    // 	Destroy(gameObject);
    // }

    void Start() {
		rb.velocity = transform.right * speed;
	}

	public void Setup(Vector2 velocity, Vector3 direction) {
		rb.velocity = velocity.normalized * speed;
		transform.rotation = Quaternion.Euler(direction);
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		if (hitInfo.CompareTag("breakable")) {
            hitInfo.GetComponent<Pot>().Smash();
        }

        if (hitInfo.gameObject.CompareTag("EnemyBoss"))
        {
            //hit.GetComponent<EnemyBoss>().currentState = EnemyState.stagger;
            Debug.Log("here");
            hitInfo.GetComponent<EnemyBoss>().TakeDamage(damage_boss_bow);
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
