using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_interact : MonoBehaviour
{

    //랜덤하게 고양이들 울게 함
    AudioClip[] catmeow = new AudioClip[3];
    GameObject AudioManager;
    Vector3 volVector;
    float effectvolume;

    Sprite[][][] catInteractSpr = new Sprite[8][][];//고양이마다

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < catInteractSpr.Length; i++)
        {
            catInteractSpr[i] = new Sprite[21][];//각각 위치 네개씩
            for (int j = 0; j < catInteractSpr[i].Length; j++)
            {
                catInteractSpr[i][j] = new Sprite[2];
                catInteractSpr[i][j][0] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_" + j);
                catInteractSpr[i][j][1] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_" + j + "_touch");


            }
        }

        //Debug.Log(catInteractSpr[0][0][1].name);

        AudioManager = GameObject.Find("AudioManager");
        catmeow = AudioManager.GetComponent<Main_AudioManager>().Cat_Meow;

        CatVolSetting();

    }

    public void CatVolSetting()
    {
        AudioManager.GetComponent<Main_AudioManager>().changeVolumn();
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        //Debug.Log("in setting, volvector is " + volVector + "and effectvolume is " + effectvolume);
    }

    public void getreaction_Cat(GameObject cat, int catnum, int posnum)
    {
        StartCoroutine(sprchange(cat, catnum, posnum));

    }

    IEnumerator sprchange(GameObject cat, int catnum, int posnum)
    {
        Debug.Log("hi??");
        int i = Random.Range(0, catmeow.Length);
        if (effectvolume != 0)
            AudioSource.PlayClipAtPoint(catmeow[i], volVector);
        Debug.Log("CATNUM IS " + catnum);
        Debug.Log("posnum is " + posnum);
        cat.GetComponent<SpriteRenderer>().sprite = catInteractSpr[catnum][posnum][1];
        //Debug.Log(catInteractSpr[catnum][0][1].name);
        yield return new WaitForSeconds(0.5f);
        cat.GetComponent<SpriteRenderer>().sprite = catInteractSpr[catnum][posnum][0];


    }
}
