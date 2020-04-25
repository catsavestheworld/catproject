using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject TotalManager;
    GameObject Manager;

    // for score
    GameObject scoreUI;
    public float timer = 0;
    public Text scoreText;
    public int seconds;
    // for present
    public Text presentText;
    public int presentNum = 0;
    // for speed
    public float speed;
    //fever
    int playTime;
    int gamePlayTime;
    int feverTime;
    public Text feverHit;
    int feverNum;
	int feverPlayTime;
    //TotalScore
    Text totalText;
    Text BestScoreText;
    int bonusWhileGame;
    //puzzle
    GameObject PuzzleSign;
    bool newPuzzle;

    //for calculation best score
    GameObject DataManager;
    int[] bestScorearr;
    int bestScore;

    int[] playnum;

    int money;

    public int totalScore;

    private void Awake()
    {
        PuzzleSign = GameObject.Find("Puzzle");
        PuzzleSign.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("Manager");
        TotalManager = GameObject.Find("Main Camera");
        DataManager = GameObject.Find("DataManager");
        scoreUI = GameObject.Find("Main_Canvas");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        presentText = GameObject.Find("SpecialScore").GetComponent<Text>();
        totalText = GameObject.Find("TotalScore").GetComponent<Text>();
        BestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        feverHit = GameObject.Find("FeverHit").GetComponent<Text>();
        bonusWhileGame = TotalManager.GetComponent<TotalManager_3>().bonusWhileGame;
		feverPlayTime = GameObject.Find("Main Camera").GetComponent<TotalManager_3>().feverPlayTime;

        //setting when fever starts
        gamePlayTime = TotalManager.GetComponent<TotalManager_3>().gamePlayTime;
        playTime = 30 + gamePlayTime;
        feverTime = 7;

        //speed of the box in main game
        speed = 0.065f;
        StartCoroutine("SpeedSetting");
        StartCoroutine("FeverSetting");
        StartCoroutine("SetScore");

        //fever related
        feverHit.text = " ";
        feverNum = 0;

        presentText.text = "0";
        BestScoreText.text = "";
        totalText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //for setting time = score
        timer += Time.deltaTime;    
    }

    //increasing speed every 30 seconds
    IEnumerator SpeedSetting()
    {
        if (speed < 0.1f)
        {
            yield return new WaitForSeconds(30);
            speed += 0.01f;
        }

        StartCoroutine(SpeedSetting());
    }

    //deciding when the fever time starts
    IEnumerator FeverSetting()
    {
        yield return new WaitForSeconds(playTime);
        Debug.Log("Fever");
        StopCoroutine("SetScore");        
        GameObject.Find("Warehouse").GetComponent<SpawnBox>().StopBox();
		yield return new WaitForSeconds(feverTime+feverPlayTime);

        StartCoroutine(FeverSetting());
    }

    public void TextPlus()
    {
        feverNum++;
        feverHit.text = feverNum + " HIT";
    }

    public void TextReset()
    {
        feverHit.text = " ";
        feverNum = 0;
    }   

    public void PresentPlus()
    {
        presentNum += 1;
        //for setting speical score (7 times at the end)
        presentText.text = presentNum.ToString();
    }

    public void FeverPresent()
    {
        Manager.GetComponent<UIManager>().presentNum += GameObject.Find("feverPresent").GetComponent<FeverTime>().presentAdd;
        presentText.text = presentNum.ToString();
    }

    //calculating the total score after the game ends
    public void CalcTotal()
    {
        double bonusAfterGame = TotalManager.GetComponent<TotalManager_3>().bonusAfterGame;
        totalScore = (int)((presentNum * 7 + seconds) * bonusAfterGame);

        money = DataManager.GetComponent<ControlGameData>().getMoney();

        //JACKPOT -- 5% possibility of having money twice when the game ends
        int i = Random.Range(0, 100);
        if (i < 5)
        {
            int jackpot = TotalManager.GetComponent<TotalManager_3>().jackpot;
            totalScore *= jackpot;
        }
        
        money += totalScore;

        playnum = DataManager.GetComponent<ControlGameData>().getPlaynum();
        playnum[2]++;
        DataManager.GetComponent<ControlGameData>().setPlaynum(playnum);
        DataManager.GetComponent<ControlGameData>().Save("playnum");

        DataManager.GetComponent<ControlGameData>().setMoney(money);
        newPuzzle = DataManager.GetComponent<PuzzleManager>().setting_Puzzle(totalScore);

        if (newPuzzle == true)
            PuzzleSign.SetActive(true);

        DataManager.GetComponent<ControlGameData>().Save("money");
        DataManager.GetComponent<ControlGameData>().Save("puzzle");

        totalText.text = "Total Score: " + totalScore.ToString();       

        bestScorearr = DataManager.GetComponent<ControlGameData>().getBestScore();
        bestScore = bestScorearr[2];

        if (bestScore < totalScore)
        {
            //Debug.Log("bestScore");
            bestScore = totalScore;
            bestScorearr[2] = bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }

        BestScoreText.text = "Best Score: " + bestScore.ToString();

        
    }

    //updating score every 0.1sec
    public IEnumerator SetScore()
    {
        yield return new WaitForSeconds(0.1f);
        seconds++;
        scoreText.text = seconds.ToString();
        StartCoroutine("SetScore");
    }

    public IEnumerator BonusScore()
    {
        yield return new WaitForSeconds(15f);
        Manager.GetComponent<UIManager>().seconds += bonusWhileGame;
        scoreText.text = seconds.ToString();
        StartCoroutine("BonusScore");
    }
}
