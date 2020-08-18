using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
   public Animator animator;

   public float attackRate = 0.1f;
   float nextAttackTime = 0f;
   

    // Update is called once per frame
    void Update()
    {
       // if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                animator.SetInteger("Skill", 1);
                animator.SetBool("Attack", true);
                nextAttackTime = Time.time + 1f / attackRate;
            }

            if (Input.GetKeyUp(KeyCode.Q)) {
                 animator.SetBool("Attack", false);
            }

            // if (Input.GetKeyDown(KeyCode.W)) {
            //     animator.SetInteger("Skill", 2);
            //     animator.SetBool("Attack", true);
            //    nextAttackTime = Time.time + 1f / attackRate;
            // }
            
            // if (Input.GetKeyUp(KeyCode.W)) {
            //     animator.SetBool("Attack", false);
            // }
    //    }

   //     else {
          //  animator.SetBool("Attack", false);
    //    }

        
         

        Debug.Log(animator.GetInteger("Skill").ToString() + " " + animator.GetBool("Attack"));
    }
}
