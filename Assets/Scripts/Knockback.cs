using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    public int damage_melee_no_weapons = 40;
    public int damage_boss_no_weapons = 1;
    public int damage_boss_weapons = 2;
	public GameObject impactEffect;

    public float damage;

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) {
            hitInfo.GetComponent<Pot>().Smash();
        }

        

        if (hitInfo.gameObject.CompareTag("EnemyBoss"))
        {
            //hit.GetComponent<EnemyBoss>().currentState = EnemyState.stagger;
            Debug.Log("here");
            hitInfo.GetComponent<EnemyBoss>().TakeDamage(damage_boss_no_weapons);
         
        }

        if (hitInfo.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("Enemy")) {
            return;
        }

        if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = hitInfo.GetComponent<Rigidbody2D>();
            

            if (hit != null) {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
                
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (hitInfo.gameObject.CompareTag("Enemy") && hitInfo.isTrigger) {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    hitInfo.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

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

   
}
