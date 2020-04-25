using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mini1_TotalManager : MonoBehaviour {

    public GameObject pauseBut;

    GameObject AudioManager;

    GameObject GAMEOVER;
    GameObject FEVER;
    GameObject MAINGAME;
    GameObject GameManager;
    GameObject FeverManager;
    GameObject Player;

    GameObject FeverPlayer;
    GameObject TouchPlayer;

    GameObject FeverScore;
    GameObject fever_text;

    Text Go;

    Sprite[] playerS;

    GameObject DataManager;
    double[] appliedEffect;

    int feverPlayTime;
    int gamePlayTime;
    public int bonusWhileGame;



    int time;
    public int gametime;
    int fevertime;

    int flag;

    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager");
    
        AudioManager.GetComponent<Main_AudioManager>().setting();
    }

    // Use this for initialization
    void Start () {
       

        playerS = new Sprite[2];

        GAMEOVER = GameObject.Find("GAMEOVER");
        FEVER = GameObject.Find("FEVER");
        MAINGAME = GameObject.Find("MAINGAME");
        GameManager = GameObject.Find("GameManager");
        FeverManager = GameObject.Find("FeverManager");
        Player = GameObject.Find("Player");
        FeverScore = GameObject.Find("FeverScore");
        fever_text = GameObject.Find("fever_text");

        FeverPlayer = Player.transform.Find("FeverPlayer").gameObject;
        TouchPlayer = GameObject.Find("TouchPlayer");

        DataManager = GameObject.Find("DataManager");
        appliedEffect = DataManager.GetComponent<GetCatEffect>().SettingCatEffect();

        //Debug.Log("appliedEffect[0]" + appliedEffect[0]);
        //Debug.Log("appliedEffect[6]" + appliedEffect[6]);


        feverPlayTime = 0 + (int)appliedEffect[4];//
        gamePlayTime = 0 + (int)appliedEffect[1];//
        bonusWhileGame = (int)appliedEffect[2]; //끗

        Go = GameObject.Find("count").GetComponent<Text>();
        Go.text = "G O !";

        fever_text.SetActive(false);
        GAMEOVER.SetActive(false);
        FEVER.SetActive(false);
        FeverPlayer.SetActive(false);

        for(int i=0; i<2; i++)
        {
            playerS[i] = Resources.Load<Sprite>("UFO_" + i);
        }

        time = 0;
        gametime = 30 +gamePlayTime;
        fevertime = 5 + feverPlayTime;

        //Debug.Log("gamePlaytime is " + gamePlayTime);
        //Debug.Log("feverPlaytime is " + feverPlayTime);

        StartCoroutine(GameManager.GetComponent<TimeScore>().CountScore());
        StartCoroutine("CheckTime");
    }
	
    IEnumerator CheckTime()
    {
        time++;

        if(time == 2)
        {
            Go.text = "";
        }

        if(time == 15)
        {
            //Debug.Log("time is " + GameManager.GetComponent<TimeScore>().time + "and bonus while game is" + bonusWhileGame);
            GameManager.GetComponent<TimeScore>().time += bonusWhileGame;
            //Debug.Log("time is " + GameManager.GetComponent<TimeScore>().time);
            GameManager.GetComponent<TimeScore>().BScore();

        }

        if (time == gametime) //피버타임 준비
        {   // 행성 바깥으로 이동 & 플레이어 중간으로 이동(터치막기)

            //일시정지 막기
            pauseBut.SetActive(false);

            Player.GetComponent<Collider2D>().enabled = false;

            fever_text.SetActive(true);

            TouchPlayer.GetComponent<PlayerMove>().check = 1;

            FeverPlayer.SetActive(true);
            GameManager.GetComponent<PlanetCreate>().Fever();
            GameManager.GetComponent<ReadyFever>().ready();
            GameManager.GetComponent<MouseCreate>().MouseOFF();

            TouchPlayer.SetActive(false);
        }


        if (gametime + 3 > time && time >= gametime + 2) //피버타임 시작
        {   // 피버타임 ON & 플레이어 이미지 변경 & 몬스터 랜덤 켜기

            fever_text.SetActive(false);

            GameManager.GetComponent<PlanetCreate>().PlanetOff();

            Player.GetComponent<SpriteRenderer>().sprite = playerS[1];
            FEVER.SetActive(true);

            FeverManager.GetComponent<FeverManager>().Boss_ON();
            GameManager.GetComponent<MapMove>().enabled = true;
            GameManager.GetComponent<TimeScore>().enabled = true;
            GameManager.GetComponent<MouseCreate>().enabled = true;
            GameManager.SetActive(false);
            MAINGAME.SetActive(false);
        }

        if (gametime+fevertime+3.5 > time && time >= gametime + fevertime+2) //피버타임 정리
        {   // 피버스코어 정지 & 플레이어 이동가능 & 원래 플레이어 이미지로;
            FeverScore.GetComponent<Collider2D>().enabled = false;
            

            FeverPlayer.SetActive(false);

            FeverManager.GetComponent<FeverManager>().Monster_back();

            Player.GetComponent<Collider2D>().enabled = true;

        }
        if (time >= gametime + fevertime+5)
        {
            Player.GetComponent<SpriteRenderer>().sprite = playerS[0];
            TouchPlayer.SetActive(true);
            TouchPlayer.GetComponent<PlayerMove>().check = 0;
            TouchPlayer.transform.position = new Vector3(0, 0, -1);

            gameObject.GetComponent<TotalScore>().FScore();

            FeverManager.GetComponent<FeverManager>().Monster_normal();
            FeverScore.GetComponent<Collider2D>().enabled = true;
            FEVER.SetActive(false);

            GameManager.SetActive(true);
            MAINGAME.SetActive(true);

            GameManager.GetComponent<PlanetCreate>().FeverOff();

            gameObject.GetComponent<CoroutineManager>().ReStart();

            Go.text = "G O !";

            time = 0;

            //일시정지 허용
            pauseBut.SetActive(true);

           
        }

        yield return new WaitForSeconds(1);
        StartCoroutine("CheckTime");
    }

    public void GameOver()
    {
        Go.text = "";
        gameObject.GetComponent<TotalScore>().TScore();
        GameManager.GetComponent<PlanetCreate>().PlanetOff();
        MAINGAME.SetActive(false);
        FEVER.SetActive(false);
        GAMEOVER.SetActive(true);
        Player.SetActive(false);
        StopAllCoroutines();
        
    }
}
