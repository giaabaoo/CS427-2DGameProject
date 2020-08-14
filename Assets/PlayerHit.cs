using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    public int damage_melee_no_weapons = 40;
    public int damage_boss_no_weapons = 1;
    public int damage_boss_weapons = 2;
    public GameObject impactEffectBoss;


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

        //EnemyMelee enemy_melee = hitInfo.GetComponent<EnemyMelee>();
        EnemyBoss enemy_boss = hitInfo.GetComponent<EnemyBoss>();
        Debug.Log("CALL TRIGGER");
		//if (enemy_melee != null)
		//{
		//	enemy_melee.TakeDamage(damage_melee_no_weapons);
		//}
        if (enemy_boss != null)
        {
            enemy_boss.TakeDamage(damage_boss_no_weapons);
        }

        //Instantiate(impactEffectMelee, transform.position, transform.rotation);
        Instantiate(impactEffectBoss, transform.position, transform.rotation);

        Destroy(gameObject);

        
    }
}
