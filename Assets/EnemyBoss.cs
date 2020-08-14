using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{


    public int threshold_hp_eye_strong = 10;
    public int threshold_hp_eye_intermediate = 8;
    public int threshold_hp_eye_weak = 6;
    public int threshold_hp_eye_die = 4;

    public int cur_hp = 15;

    public GameObject deathEffect;
    public Animator animator;

    public void TakeDamage(int damage)
    {
        cur_hp -= damage;
//        Debug.Log(cur_hp.ToString());
        if (cur_hp == threshold_hp_eye_strong)
        {
            State1();
        }
        else if (cur_hp == threshold_hp_eye_intermediate)
        {
            //Die();
        }
        else if (cur_hp == threshold_hp_eye_weak)
        {
            //Die();
        }
        else if (cur_hp == threshold_hp_eye_die)
        {
            //Die();
        }
        else if (cur_hp == 0)
        {
            Die();
        }
    }
    void State1()
    {
        //Debug.Log("state1");
        animator = GetComponent<Animator>();

        animator.Play("eye_strong_open_to_close", -1, 0f);
        animator.Play("eye_intermediate_close_to_open");
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
