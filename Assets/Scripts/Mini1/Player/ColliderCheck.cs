using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderCheck : MonoBehaviour {

    public Text mouse_score;
    public int count;
    private Vector3 localpos;
    GameObject SpaceMouse;

    GameObject TotalManager;

    GameObject AudioManager;
    AudioClip cat_hit;
    AudioClip mouse_get;
    Vector3 volVector;
    float effectvolume;

    void Start()
    {
        TotalManager = GameObject.Find("TotalManager");
        count = 0;
        SpaceMouse = GameObject.Find("SpaceMouse");

        mouse_score.text = " : 0";

        AudioManager = GameObject.Find("AudioManager");
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;
        cat_hit = AudioManager.GetComponent<Main_AudioManager>().cat_hit;
        mouse_get = AudioManager.GetComponent<Main_AudioManager>().mouse_get;
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        switch (col.gameObject.tag)
        {
            case "SpaceMouse":
                if (effectvolume != 0)
                {
                    AudioSource.PlayClipAtPoint(mouse_get, volVector);
                }

                count++;
                localpos = SpaceMouse.transform.position;
                localpos.x = 18f;
                //SpaceMouse.transform.position = localpos;
                //SpaceMouse.SetActive(false);
                col.gameObject.transform.position = localpos;
                col.gameObject.SetActive(false);
                mouse_score.text = " : " + count.ToString();
                break;

            case "Planet":
                if (effectvolume != 0)
                {
                    AudioSource.PlayClipAtPoint(cat_hit, volVector);
                }

                TotalManager.GetComponent<mini1_TotalManager>().GameOver();
                break;
        }

      

    }
}
