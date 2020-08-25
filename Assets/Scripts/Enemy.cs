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

	public FloatValue maxHealth;
    public float health;

	public GameObject deathEffect;
	public LootTable lootItem;

	public string enemyName;
	public int baseAttack;
	public float moveSpeed;

	private void Awake() {
		health = maxHealth.initialValue;
	}

	public void Knock(Rigidbody2D rb, float knockTime, float damage) {
		StartCoroutine(KnockCo(rb, knockTime));
		TakeDamage(damage);
	}

 	private IEnumerator KnockCo(Rigidbody2D rb, float knockTime) {
        if (rb != null) {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
			rb.velocity = Vector2.zero;
        }
    }

	public void TakeDamage (float damage)
	{
		health -= damage;
		//Debug.Log("Current enemy health: "+health.ToString());

		if (health <= 0)
		{
			Die();
			MakeLoot();
			this.gameObject.SetActive(false);
		}
	
	}

	private void MakeLoot()
    {
        if(lootItem != null)
        {
            Powerup current = lootItem.LootPowerup();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

	void Die ()
	{
		if (deathEffect != null) {
			GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(effect, 1f);
		}
	}
}
