using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    public int damage = 40;
    public GameObject impactEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("breakable")) {
            hitInfo.GetComponent<Pot>().Smash();
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
		Instantiate(impactEffect, transform.position, transform.rotation);

		Destroy(gameObject);

        
    }
}
