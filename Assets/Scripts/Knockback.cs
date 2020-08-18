using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) {
            hitInfo.GetComponent<Pot>().Smash();
        }

        if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = hitInfo.GetComponent<Rigidbody2D>();
            

            if (hit != null) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (hitInfo.gameObject.CompareTag("Enemy")) {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    hitInfo.GetComponent<Enemy>().Knock(hit, knockTime);
                }

                if (hitInfo.gameObject.CompareTag("Player")) {
                    hit.GetComponent<PlayerController>().currentState = PlayerState.stagger;
                    hitInfo.GetComponent<PlayerController>().Knock(knockTime);
                }
               
            }
        }
    }

   
}
