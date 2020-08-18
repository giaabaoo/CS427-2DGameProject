using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
	idle,
	walk,
	attack,
	stagger
}
public class Enemy : MonoBehaviour
{
	public EnemyState currentState;
    public int health = 100;

	public GameObject deathEffect;

	public string enemyName;
	public int baseAttack;
	public float moveSpeed;

	public void Knock(Rigidbody2D rb, float knockTime) {
		StartCoroutine(KnockCo(rb, knockTime));
	}

 	private IEnumerator KnockCo(Rigidbody2D rb, float knockTime) {
        if (rb != null) {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
			rb.velocity = Vector2.zero;
        }
    }

	public void TakeDamage (int damage)
	{
		health -= damage;
		Debug.Log("Current health: "+health.ToString());

		if (health <= 0)
		{
			Die();
		}
	
	}

	void Die ()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
