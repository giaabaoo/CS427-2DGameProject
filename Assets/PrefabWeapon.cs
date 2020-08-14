using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        string current_weapon_id = animator.GetInteger("Weapon").ToString(); 

        if (Input.GetKeyDown(KeyCode.Q) && current_weapon_id == "2") {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}
