using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}
