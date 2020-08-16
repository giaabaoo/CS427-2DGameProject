using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    public float speed = 1f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;


    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("breakable")) {
            hitInfo.GetComponent<Pot>().Smash();
        }


        Enemy enemy = hitInfo.GetComponent<Enemy>();
		if (enemy != null)
		{
            Debug.Log("Hit enemy");

			enemy.TakeDamage(damage);

		}
		Instantiate(impactEffect, transform.position, transform.rotation);

		//Destroy(gameObject);

        
    }
}
