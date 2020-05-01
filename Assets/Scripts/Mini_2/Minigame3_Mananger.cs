using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame3_Mananger : MonoBehaviour {

    public GameObject gameUI;
    public GameObject pauseBut;

    GameObject AudioManager;
    GameObject DataManager;
    GameObject FeverSign;

    public int speacialscore;
    public int normalscore;
    public double finalScore;

    float playTime;
    float feverTime;

    int[] buycat;
    public List<int> boughtCat = new List<int>();
    int tempcatindex;

    Text NormalScoreText;
    Text SpecialScoreText;
    Text FeverText;
    Text FinalScoreText;
    Text BestScoreText;

    GameObject Game;
    GameObject FeverTime;
    GameObject GameOver;

    GameObject GameOverScore;

    GameObject catmanagerObj;
    GameObject speacialScoreObj;

    GameObject GameManager;

    GameObject[] FeverCat = new GameObject[3];

    double[] appliedEffect;

    double bonusAfterGame;
    int feverPlayTime;
    int gamePlayTime;
    public int bonusWhileGame;
    int bonusAfterFever;
    int jackpot;


    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager");
        AudioManager.GetComponent<Main_AudioManager>().setting();

        DataManager = GameObject.Find("DataManager");
        DataManager.GetComponent<ControlGameData>().Load();
    }

    // Use this for initialization
    void Start () {

        Time.timeScale = 1;
        
        buycat = DataManager.GetComponent<ControlGameData>().getBuycat();
        tempcatindex = 0;
        boughtCat.Add(0);

        for (int i = 0; i < buycat.Length; i++)
        {
            if (buycat[i] != -1)
                boughtCat.Add(i);
        }

        appliedEffect = DataManager.GetComponent<GetCatEffect>().SettingCatEffect();

        bonusAfterGame = 1 + appliedEffect[0] + appliedEffect[6];
        feverPlayTime = 0 + (int)appliedEffect[4];
        gamePlayTime = 0 + (int)appliedEffect[1];
        bonusWhileGame = (int)appliedEffect[2];
        bonusAfterFever = (int)appliedEffect[3];
        jackpot = (int)appliedEffect[5];

        GameManager = GameObject.Find("GameManager");
        if (bonusWhileGame != 0)
        {
            GameManager.GetComponent<TotalManager>().gameBonus = bonusWhileGame;
            StartCoroutine(GameManager.GetComponent<TotalManager>().GameBonus());
        }
            

        FeverSign = GameObject.Find("Fever_Sign");
        FeverSign.SetActive(false);

        speacialscore = 0;
        normalscore = 0;

        playTime = 30 + gamePlayTime;
        feverTime = 5 + feverPlayTime;

        GameOverScore = GameObject.Find("GameOverImg");
        FinalScoreText = GameObject.Find("FinalScore").GetComponent<Text>();
        FinalScoreText.text = "";

        NormalScoreText = GameObject.Find("NormalScore").GetComponent<Text>();
        SpecialScoreText = GameObject.Find("SpecialScore").GetComponent<Text>();
        FeverText = GameObject.Find("FeverText").GetComponent<Text>();
        BestScoreText = GameObject.Find("BestScore").GetComponent<Text>();

        FeverText.text = "";
        BestScoreText.text = "";

        Game = GameObject.Find("Game");
        FeverTime = GameObject.Find("FeverTime");
        GameOver = GameObject.Find("GameOver");

        FeverCat[0] = FeverTime.transform.Find("Cat").transform.Find("fevercat1").gameObject;
        FeverCat[1] = FeverTime.transform.Find("Cat").transform.Find("fevercat2").gameObject;
        FeverCat[2] = FeverTime.transform.Find("Cat").transform.Find("fevercat3").gameObject;

        for (int i = 0; i < 3; i++)
        {
            string newfeverfiledir = "";
            int j = Random.Range(0, boughtCat.Count);
            tempcatindex = boughtCat[j];
            string feverfiledir = "Pic_3/Cat/Cat_" + tempcatindex.ToString() + "/Cat_";
            switch (i)
            {
                case 0:
                    newfeverfiledir = feverfiledir + "happy";
                    break;
                case 1:
                    newfeverfiledir = feverfiledir + "normal";
                    break;
                case 2:
                    newfeverfiledir = feverfiledir + "good";
                    break;

            }
            FeverCat[i].GetComponent<Fever_ShakingTail>().SettingFeverSprite(newfeverfiledir) ;
        }

        catmanagerObj = Game.transform.Find("CatManager").gameObject;
        speacialScoreObj = FeverTime.transform.Find("Background").gameObject;

        Game.SetActive(true);
        FeverTime.SetActive(false);
        GameOver.SetActive(false);

        StartCoroutine("Fever");
       
	}

    public void callFeverTime()
    {
        //일시정지 비허용
        pauseBut.SetActive(false);

        if (catmanagerObj.GetComponent<CatManager>().realWaitTime >= 0)
            catmanagerObj.GetComponent<CatManager>().waitTime = catmanagerObj.GetComponent<CatManager>().realWaitTime;
        catmanagerObj.GetComponent<CatManager>().nowWait = false;

        Game.SetActive(false);
        FeverTime.SetActive(true);
        FeverText.text = "0 HIT!";

        for(int i = 0; i < 3; i++)
        {
            FeverCat[i].GetComponent<Fever_ShakingTail>().fever_tail();
        }
        
    }

    public void backtoGame()
    {
        //일시정지 허용
        pauseBut.SetActive(true);

        speacialscore += speacialScoreObj.GetComponent<FeverTimeScript>().touchnum;
        speacialScoreObj.GetComponent<FeverTimeScript>().touchnum = 0;
        normalscore += bonusAfterFever;
        showNormalscore();
        showSpeacialscore();
        FeverText.text = "";
        FeverTime.SetActive(false);

        Game.SetActive(true);
        Game.GetComponent<InvokeCoroutine>().InvokingCoroutine();
    }

    public void callGameover()
    {
        GameOver.SetActive(true);
        gameUI.SetActive(false);
        calculFinalScore();
        GameOverScore.GetComponent<AppearScore>().setFinalScore();
        Game.SetActive(false);
        FeverTime.SetActive(false);
        StopCoroutine("Fever");
    }

    public void showNormalscore()
    {
        NormalScoreText.text = normalscore.ToString();
    }

    public void showSpeacialscore()
    {
        SpecialScoreText.text = speacialscore.ToString();
    }

    public void calculFinalScore()
    {
        finalScore = (normalscore + speacialscore * 7) * bonusAfterGame;
        int i = Random.Range(0, 100);
        if (i < 5)
            finalScore *= jackpot;
    }

    IEnumerator Fever()
    {
        yield return new WaitForSeconds(playTime);
        FeverSign.SetActive(true);
        yield return new WaitForSeconds(1f);
        FeverSign.SetActive(false);
        callFeverTime();
        yield return new WaitForSeconds(feverTime);
        backtoGame();

        StartCoroutine("Fever");
    }
}
