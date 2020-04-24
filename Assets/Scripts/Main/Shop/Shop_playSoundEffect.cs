using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_playSoundEffect : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject AudioManager;
    Vector3 volVector;
    float effectvolume;
    AudioClip catEffect;
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
    }

    void getSetting(){
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;
        catEffect = AudioManager.GetComponent<Main_AudioManager>().Cat_Meow[0];
    }

    // Update is called once per frame
    private void OnMouseDown() {
        if(catEffect == null){
            getSetting();

        }
        
        if (effectvolume != 0){
            AudioSource.PlayClipAtPoint(catEffect, volVector);
        }
    }
}
