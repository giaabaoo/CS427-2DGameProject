using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{


    public int threshold_hp_eye_strong = 23;
    public int threshold_hp_eye_intermediate = 16;
    public int threshold_hp_eye_weak = 9;
    public int threshold_hp_eye_die = 0;

    int cur_hp = 30;

    public GameObject deathEffect;
    public Animator animator;

    public void TakeDamage(int damage)
    {
        cur_hp -= damage;
        Debug.Log(cur_hp.ToString());
        if (cur_hp <= threshold_hp_eye_strong && cur_hp > threshold_hp_eye_intermediate)
        {
            Debug.Log("Change");
            State1();
            return;
        }
        else if (cur_hp <= threshold_hp_eye_intermediate && cur_hp > threshold_hp_eye_weak)
        {
            State2();
            return;
        }
        else if (cur_hp <= threshold_hp_eye_weak && cur_hp > threshold_hp_eye_die)
        {
            State3();
            return;
        }
        else if (cur_hp <= threshold_hp_eye_die)
        {
            Die();
            return;
        }
    }
    void State1()
    {
        //Debug.Log("state1");
        animator.SetBool("strong_to_intermediate", true);
    }

    void State2()
    {
        //Debug.Log("state1");
        animator.SetBool("intermediate_to_weak", true);
    }

    void State3()
    {
        //Debug.Log("state1");
        animator.SetBool("weak_to_die", true);
    }


    void State4()
    {
        //Debug.Log("state1");
        animator.SetBool("", true);
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
