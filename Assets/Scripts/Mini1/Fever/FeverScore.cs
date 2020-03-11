using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverScore : MonoBehaviour {

    // public int total_Fscore; //모든 총 피버타임 점수의 합 
    public int Fscore;
    public Text fever_score;

    GameObject[] Monster;
    GameObject Player;
    GameObject Lazer;

    Sprite[] ufoS;
    Sprite[] monsterS;

    GameObject AudioManager;
    AudioClip fever_laser;
    Vector3 volVector;
    float effectvolume;

    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;
        fever_laser = AudioManager.GetComponent<Main_AudioManager>().fever_laser;

        // total_Fscore = 0;
        Fscore = 0;
        Monster = new GameObject[4];
        ufoS = new Sprite[2];
        monsterS = new Sprite[8];

        Lazer = GameObject.Find("UFO_fever_lazer");
        Player = GameObject.Find("Player");

        Lazer.SetActive(false);

        for (int i = 0; i < 2; i++)
        {
            ufoS[i] = Resources.Load<Sprite>("UFO_" + (i + 1));
        }
        for( int i=0; i<4; i++)
        {
            Monster[i] = GameObject.Find("Monster_" + i);
            monsterS[i] = Resources.Load<Sprite>("Monster_" + i);
            monsterS[(i+4)] = Resources.Load<Sprite>("Monster_" + i + "_hit");
        }

        
    }

    void OnMouseDown()
    {
        Fscore++;
        // total_Fscore++;
        Lazer.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            Monster[i].GetComponent<SpriteRenderer>().sprite = monsterS[(i + 4)];
        }
        Player.GetComponent<SpriteRenderer>().sprite = ufoS[1];
        fever_score.fontSize = 200;
        fever_score.text = Fscore.ToString() + " H IT";

        if (effectvolume != 0)
        {
            AudioSource.PlayClipAtPoint(fever_laser, volVector);
        }
    }

    void OnMouseUp()
    {
        fever_score.fontSize = 150;
        Lazer.SetActive(false);
        for(int i=0; i<4; i++)
        {
            Monster[i].GetComponent<SpriteRenderer>().sprite = monsterS[i];
        }
        Player.GetComponent<SpriteRenderer>().sprite = ufoS[0];
    }

    public void ReFeverScore()
    {
        Fscore = 0;
        fever_score.fontSize = 150;
        fever_score.text = "0 H IT";
    }
}
