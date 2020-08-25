using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;
    public LootTable lootItem;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash() {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
        MakeLoot();
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

    IEnumerator breakCo() {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }


}
