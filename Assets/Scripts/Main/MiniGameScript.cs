using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameScript : CommonJob
{

    GameObject IntroObj;
    GameObject selectObj;

    GameObject[] selectColli = new GameObject[3];
    GameObject miniGame_Goout;

    GameObject HowtoObj;
    GameObject[] HowtoArrow = new GameObject[2];
    GameObject HowtoExit;

    Sprite[][] HowtoSpr = new Sprite[3][];
    int nowHowto;
    int sprIndex;

    string[] scene = new string[3];
    int[] playnum = new int[3];

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        IntroObj = GameObject.Find("Intro_Story");
        selectObj = GameObject.Find("MiniGame_Select");

        selectColli[0] = GameObject.Find("MiniGame_1st");
        selectColli[1] = GameObject.Find("MiniGame_2nd");
        selectColli[2] = GameObject.Find("MiniGame_3rd");
        miniGame_Goout = GameObject.Find("MiniGame_goout");

        HowtoObj = GameObject.Find("MiniGame_Howto");
        HowtoArrow[0] = GameObject.Find("Howto_RightArrow");
        HowtoArrow[1] = GameObject.Find("Howto_LeftArrow");

        for(int i = 0; i < HowtoSpr.Length; i++)
        {
            HowtoSpr[i] = new Sprite[2];
            for(int j = 0; j < 2; j++)
            {
                HowtoSpr[i][j] = Resources.Load<Sprite>("Main/Howto/howto_"+(i+1)+"_"+j);
            }

        }

        scene[0] = "Mini_1";
        scene[1] = "Mini_2";
        scene[2] = "Mini_3";

        HowtoObj.SetActive(false);

        selectObj.SetActive(false);

    }

    public override void initial()
    {
        //각 미니게임별 플레이 횟수를 읽어오기
        selectObj.SetActive(true);
        playnum = DataManager.GetComponent<ControlGameData>().getPlaynum();

    }

    public void showHowto(int gameNum)
    {
        offSelectColli();
        HowtoObj.SetActive(true);
        HowtoObj.GetComponent<SpriteRenderer>().sprite = HowtoSpr[gameNum-1][0];
        nowHowto = gameNum-1;
        sprIndex = 0;
        HowtoArrow[sprIndex].SetActive(true);
        HowtoArrow[(sprIndex+1) % 2].SetActive(false);
    }

    public void showAnotherspr(string dir)
    {
        
        switch (dir)
        {
            case "LeftArrow":
                if(sprIndex == 1)
                {
                    HowtoObj.GetComponent<SpriteRenderer>().sprite = HowtoSpr[nowHowto][0];
                    sprIndex = 0;
                }
                break;
            case "RightArrow":
                if (sprIndex == 0)
                {
                    HowtoObj.GetComponent<SpriteRenderer>().sprite = HowtoSpr[nowHowto][1];
                    sprIndex = 1;
                }
                break;
        }
        HowtoArrow[sprIndex].SetActive(true);
        HowtoArrow[(sprIndex + 1) % 2].SetActive(false);
    }

    public void offHowto()
    {
        HowtoObj.SetActive(false);
        onSelectColli();
    }

    void offSelectColli()
    {
        miniGame_Goout.GetComponent<BoxCollider2D>().enabled = false;
        for (int i = 0; i < 3; i++)
            selectColli[i].GetComponent<BoxCollider2D>().enabled = false;
    }

    void onSelectColli()
    {
        miniGame_Goout.GetComponent<BoxCollider2D>().enabled = true;
        for (int i = 0; i < 3; i++)
            selectColli[i].GetComponent<BoxCollider2D>().enabled = true;
    }

    public void playGame(int clickindex)
    {
        
        if (playnum[clickindex] == 0)
        {
            selectObj.SetActive(false);
            playnum[clickindex]++;
            IntroObj.SetActive(true);
            IntroObj.GetComponent<ShowingIntro>().callingIntro(gameObject, clickindex);
        }
        else
        {
            selectObj.SetActive(false);
            //로딩화면 띄우고
            IntroObj.SetActive(true);
            IntroObj.GetComponent<ShowingIntro>().showLoading(clickindex);
            //게임으로 가기!
            SceneManager.LoadScene(scene[clickindex]);
        }

    }

    //x표 클릭한 경우 --> goout
    public void goout()
    {
        selectObj.SetActive(false);
        finish();
    }

    public override void finish()
    {
        base.finish();
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }
}

