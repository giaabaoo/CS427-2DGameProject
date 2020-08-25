using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip boss_soundtrack, playerhit_soundtrack;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        boss_soundtrack = Resources.Load<AudioClip> ("Boss");

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
        }
    }
}
