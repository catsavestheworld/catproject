using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalManager_3 : MonoBehaviour
{
    GameObject AudioManager;
    GameObject mainGame;
    GameObject feverTime;
    GameObject feverPresent;
    GameObject gameOver;
    GameObject gameOverScene;
    GameObject UIManager;

    GameObject dragFlag;
    GameObject feverTimeFrame;

    Sprite[] gameoverSpr = new Sprite[2];

    //고양이 부가효과
    GameObject DataManager;
    double[] appliedEffect;

    public double bonusAfterGame;
    public int feverPlayTime;
    public int gamePlayTime;
    public int bonusWhileGame;
    public int bonusAfterFever;
    public int jackpot;

    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager");
        AudioManager.GetComponent<Main_AudioManager>().setting();

        DataManager = GameObject.Find("DataManager");
        DataManager.GetComponent<ControlGameData>().Load();
    }

    // Use this for initialization
    void Start()
    {
        appliedEffect = DataManager.GetComponent<GetCatEffect>().SettingCatEffect();

        //게임 끝나고 score 에 곱해주는 배수
        bonusAfterGame = 1 + appliedEffect[0] + appliedEffect[6];//UIManager
        //fever 타임을 얼마나 늘릴건지
        feverPlayTime = 0 + (int)appliedEffect[4];//FeverTime
        //playtime 얼마나 줄일지 (꼭 더하기 사용)
        gamePlayTime = 0 + (int)appliedEffect[1];
        //게임 15초마다 추가되는 보너스 (normal score)
        bonusWhileGame = (int)appliedEffect[2]; //UIManager
        //피버 끝나고 더해지는 보너스 (normal)
        bonusAfterFever = (int)appliedEffect[3]; //FeverTime
        //게임 끝나고 5퍼 확률로 2배 
        jackpot = (int)appliedEffect[5];//UIManager

        Debug.Log("bonusaftergame is " + bonusAfterGame);
        Debug.Log("feverPlaytime is" + feverPlayTime);
        Debug.Log("gameplaytime is " + gamePlayTime);
        Debug.Log("bonuswhilegame is " + bonusWhileGame);
        Debug.Log("bonusafterfever is " + bonusAfterFever);
        Debug.Log("jackpot is " + jackpot);

        mainGame = GameObject.Find("MainGame");
        feverTime = GameObject.Find("FeverTime");
        feverPresent = GameObject.Find("feverPresent");
        gameOver = GameObject.Find("GameOver");
        UIManager = GameObject.Find("Manager");

        if (bonusWhileGame != 0)
        {
            StartCoroutine(UIManager.GetComponent<UIManager>().BonusScore());
        }

        for (int i = 0; i < 2; i++)
        {
            gameoverSpr[i] = Resources.Load<Sprite>("Sprites/GameOver_box_" + (i+1));
        }
        gameOverScene = GameObject.Find("GameOverScene");

        feverTimeFrame = GameObject.Find("FeverTimeFrame");
        dragFlag = GameObject.Find("Drag_sign");

        mainGame.SetActive(true);
        feverTime.SetActive(false);
        gameOver.SetActive(false);
        feverTimeFrame.SetActive(false);
    }

    //turn on FEVER
    public void IsFever()
    {
        feverTime.SetActive(true);
        feverPresent.SetActive(true);
        GameObject.Find("feverPresent").GetComponent<FeverTime>().fevercall = false;
        Debug.Log("istrue");
        feverPresent.GetComponent<FeverTime>().presentAdd = 0;
        feverPresent.GetComponent<FeverTime>().SettingPos();
        mainGame.SetActive(false);                
    }

    //back to main game
    public void MainGameOn()
    {
        StartCoroutine(GameObject.Find("Manager").GetComponent<UIManager>().SetScore());
        UIManager.GetComponent<UIManager>().FeverPresent();
        feverTime.SetActive(false);
        mainGame.SetActive(true);
        dragFlag.SetActive(true);
        GameObject.Find("Warehouse").GetComponent<SpawnBox>().CallBox();
    }

    //when cat falls or go downward
    public void CatEnd()
    {
        mainGame.SetActive(false);
        gameOver.SetActive(true);
        feverTime.SetActive(false);
        gameOverScene.GetComponent<SpriteRenderer>().sprite = gameoverSpr[0];
        UIManager.GetComponent<UIManager>().CalcTotal();
        UIManager.SetActive(false);
    }

    //when toy falls or go upward
    public void ToyEnd()
    {
        mainGame.SetActive(false);
        gameOver.SetActive(true);
        feverTime.SetActive(false);
        gameOverScene.GetComponent<SpriteRenderer>().sprite = gameoverSpr[1];
        UIManager.GetComponent<UIManager>().CalcTotal();
        UIManager.SetActive(false);
    }
}
