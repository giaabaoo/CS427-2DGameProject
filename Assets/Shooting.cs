using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreaDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;




    public Transform player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) > retreaDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) > retreaDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

}















    // Start is called before the first frame update
    //public Transform firePoint;
    //public GameObject bulletPrefab;

    //public float bulletForce = 20f;

    //// Update is called once per frame
    //void Update()
    //{
    //    // start to shoot here
    //    if(Input.GetButtonDown("Fire1"))
    //    {
    //        Shoot();  
    //    }
    //}
    
    //void Shoot()
    //{
    //    GameObject  bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); 
    //}
