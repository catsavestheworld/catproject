using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//BGM 플레이해주는것.
public class Main_AudioManager : MonoBehaviour
{
    GameObject Listener;
    GameObject DataManager;

    int[][] volumn = new int[4][];

    int nowScene;

    float bgmVol;
    public float effectVol;
    public Vector3 effectVector;

    AudioSource audioPlay;

    //Main
    AudioClip MainBGM;
    public AudioClip Shop_buy;
    public AudioClip Shop_cannotbuy;
    public AudioClip[] Cat_Meow = new AudioClip[3];

    //mini1
    AudioClip Mini1BGM;
    public AudioClip cat_hit;
    public AudioClip fever_laser;
    public AudioClip mouse_get;

    //Mini2
    AudioClip Mini2BGM;
    public AudioClip furdisappear;
    public AudioClip cat_feelingGood;
    public AudioClip cat_feelingBad;
    public AudioClip fever_toySwing;

    //Mini3
    AudioClip Mini3BGM;
    public AudioClip PresentClicking;
    public AudioClip PutBoxDown;
    public AudioClip CatCrying;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        
        
        audioPlay = gameObject.GetComponent<AudioSource>();
        DataManager = GameObject.Find("DataManager");

        //bgm
        MainBGM = Resources.Load<AudioClip>("Sounds/Main/Main_BGM");
        Mini1BGM = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_BGM");
        Mini2BGM = Resources.Load<AudioClip>("Sounds/Mini2/Mini2_BGM");
        Mini3BGM = Resources.Load<AudioClip>("Sounds/Mini3/Mini3_BGM");

        //main effect
        Shop_buy = Resources.Load<AudioClip>("Sounds/Main/Shop_Buy");
        Shop_cannotbuy = Resources.Load<AudioClip>("Sounds/Main/Shop_CannotBuy");
        Cat_Meow[0] = Resources.Load<AudioClip>("Sounds/Main/Cat_meow_0");
        Cat_Meow[1] = Resources.Load<AudioClip>("Sounds/Main/Cat_meow_1");
        Cat_Meow[2] = Resources.Load<AudioClip>("Sounds/Main/Cat_meow_2");

        //mini1 effect
        cat_hit = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_hit");
        fever_laser = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_laser");
        mouse_get = Resources.Load<AudioClip>("Sounds/Mini1/Mini1_rat");

        //mini2 effect
        furdisappear = Resources.Load<AudioClip>("Sounds/Mini2/fur_disappear");
        cat_feelingBad = Resources.Load<AudioClip>("Sounds/Mini2/Cat_feelingBad");
        cat_feelingGood = Resources.Load<AudioClip>("Sounds/Mini2/Cat_feelingGood");
        fever_toySwing = Resources.Load<AudioClip>("Sounds/Mini2/Fever_ToySwing");

        //mini3 effect
        PresentClicking = Resources.Load<AudioClip>("Sounds/Mini3/PresentClicking");
        PutBoxDown = Resources.Load<AudioClip>("Sounds/Mini3/PutBoxDown"); ;
        CatCrying = cat_feelingGood;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                Listener = GameObject.Find("MainCamera");
                audioPlay.clip = MainBGM;
                nowScene = 0;
                break;
            case "Mini_1":
                Listener = GameObject.Find("Main Camera");
                audioPlay.clip = Mini1BGM;
                nowScene = 1;
                break;
            case "Mini_2":
                Listener = GameObject.Find("MainCamera");
                audioPlay.clip = Mini2BGM;
                nowScene = 2;
                break;
            case "Mini_3":
                Listener = GameObject.Find("Main Camera");
                audioPlay.clip = Mini3BGM;
                nowScene = 3;
                break;
        }

        audioPlay.Play();
        // setting();
        for (int i = 0; i < 4; i++)
            volumn[i] = new int[2];
        changeVolumn();
    }

    public void setting()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                Listener = GameObject.Find("MainCamera");
                audioPlay.clip = MainBGM;
                nowScene = 0;
                break;
            case "Mini_1":
                Listener = GameObject.Find("Main Camera");
                audioPlay.clip = Mini1BGM;
                nowScene = 1;
                break;
            case "Mini_2":
                Listener = GameObject.Find("MainCamera");
                audioPlay.clip = Mini2BGM;
                nowScene = 2;
                break;
            case "Mini_3":
                Listener = GameObject.Find("Main Camera");
                audioPlay.clip = Mini3BGM;
                nowScene = 3;
                break;
        }

        changeVolumn();
        audioPlay.Play();
    }

    public void changeVolumn()
    {
        int[] tempvolumn;

        tempvolumn = DataManager.GetComponent<ControlGameData>().getVolumn();

        for (int i = 0; i < 4; i++)
        {
            volumn[i][0] = tempvolumn[i] / 10; // 배경음악 볼륨
            volumn[i][1] = tempvolumn[i] % 10; // 효과음 볼륨
        }

        bgmVol = volumn[nowScene][0]; // --> 5,6,7,8,9 vol%5+1
        effectVol = volumn[nowScene][1];

        if (bgmVol != 0)
            bgmVol = ((bgmVol % 5) + 1) / 5;

        if (effectVol != 0)
            effectVol = ((effectVol % 5) + 1) / 5;

        audioPlay.volume = bgmVol;

        effectVector = new Vector3(Listener.transform.position.x, 0, -(0 + effectVol * 10));
    }
}