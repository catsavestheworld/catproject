using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

    GameObject FeverScore;
    GameObject GameManager;
    GameObject DataManager;
    GameObject Player;
    GameObject Puzzle;

    int[] bestScorearr;
    int bestScore;

    public Text best_score;
    public int totalS;
    int money;

    double bonusAfterGame;
    double[] appliedEffect;
    int bonusAfterFever;
    int jackpot;

    int[] playnum;

    bool new_puzzle;

    public Text total_score;


    void Awake () {
        DataManager = GameObject.Find("DataManager");
        FeverScore = GameObject.Find("FeverScore");
        GameManager = GameObject.Find("GameManager");
        Player = GameObject.Find("Player");

        total_score = GameObject.Find("score_Total").GetComponent<Text>();
        best_score = GameObject.Find("score_Best").GetComponent<Text>();

        total_score.text = "score : 0";

        appliedEffect = DataManager.GetComponent<GetCatEffect>().SettingCatEffect();
        bonusAfterGame = 1 + appliedEffect[0] + appliedEffect[6];
        bonusAfterFever = (int)appliedEffect[3];
        jackpot = (int)appliedEffect[5];


        Puzzle = GameObject.Find("Puzzle");
        Puzzle.SetActive(false);
        new_puzzle = false;
    }
	
    public void FScore()
    {
        int tempF;
        tempF = FeverScore.GetComponent<FeverScore>().Fscore;
        GameManager.GetComponent<TimeScore>().time += tempF * 7 + bonusAfterFever ;
    }

 

	public void TScore()
    {
        int mainS, mouseS;
        
        mainS = GameManager.GetComponent<TimeScore>().time;
        mouseS = Player.GetComponent<ColliderCheck>().count;

        bestScorearr = DataManager.GetComponent<ControlGameData>().getBestScore();
        bestScore = bestScorearr[0];

        money = DataManager.GetComponent<ControlGameData>().getMoney();
        totalS = (int)((mainS + mouseS * 100 )* bonusAfterGame);
   
        int i = Random.Range(0, 100);
        if (i < 5)
            totalS *= jackpot;

        money += (int)totalS;

       

        if (bestScore < totalS)
        {
            bestScore = totalS;
            bestScorearr[0] = (int)bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }

        playnum = DataManager.GetComponent<ControlGameData>().getPlaynum();
        playnum[0]++;
        DataManager.GetComponent<ControlGameData>().setPlaynum(playnum);
        DataManager.GetComponent<ControlGameData>().Save("playnum");



        DataManager.GetComponent<ControlGameData>().setMoney(money);
        new_puzzle = DataManager.GetComponent<PuzzleManager>().setting_Puzzle(totalS);
        DataManager.GetComponent<ControlGameData>().Save("money");
        DataManager.GetComponent<ControlGameData>().Save("puzzle");

        if (new_puzzle == true)
            Puzzle.SetActive(true);

        best_score.text = "Best Score : " + bestScore.ToString();
        total_score.text = "Total Score : " + totalS.ToString();
    }


}
