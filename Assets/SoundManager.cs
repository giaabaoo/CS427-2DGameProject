using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip boss_soundtrack, playerhit_soundtrack, bow_fire_arrow, boomerang, spear;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        boss_soundtrack = Resources.Load<AudioClip> ("Boss");
        playerhit_soundtrack = Resources.Load<AudioClip> ("punch");
        bow_fire_arrow = Resources.Load<AudioClip> ("bow_fire_arrow");
        boomerang = Resources.Load<AudioClip>("boomerang");
        spear = Resources.Load<AudioClip>("spear");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip){
        switch(clip){
            case "boss":
                audioSrc.PlayOneShot(boss_soundtrack);
                break;
            case "punch":
                audioSrc.PlayOneShot(playerhit_soundtrack);
                break;
            case "bow_fire_arrow":
                audioSrc.PlayOneShot(bow_fire_arrow);
                break;
            case "boomerang":
                audioSrc.PlayOneShot(boomerang);
                break;
            case "spear":
                audioSrc.PlayOneShot(spear);
                break;
        }
    }
}
