using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Powerup
{
    public Inventory playerInventory;
    public Item contents;


    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.AddItem(contents);
            playerInventory.currentItem = contents;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }

    }
}
