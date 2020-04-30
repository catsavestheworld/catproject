using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearScore : MonoBehaviour {
    Text ScoreText;
    Text BestScoreText;

    GameObject GameManager;
    GameObject DataManager;

    GameObject PuzzleSign;

    public int finalScore;
    int[] bestScorearr;
    int bestScore;

    int[] playnum;

    bool newPuzzle;

    int money;

    private void Awake()
    {
        PuzzleSign = GameObject.Find("Puzzle");
        PuzzleSign.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        GameManager = GameObject.Find("MainCamera");
        DataManager = GameObject.Find("DataManager");
        

        BestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        ScoreText = GameObject.Find("FinalScore").GetComponent<Text>();
    }
	
	public void setFinalScore()
    {
        Start();

        newPuzzle = false;

        bestScorearr = DataManager.GetComponent<ControlGameData>().getBestScore();
        bestScore = bestScorearr[1];

        finalScore = (int)GameManager.GetComponent<Minigame3_Mananger>().finalScore;

        money = DataManager.GetComponent<ControlGameData>().getMoney();
        money += finalScore;

        //best score의 경우는 여기서 save 진행.
        if (bestScore < finalScore)
        {
            bestScore = finalScore;
            bestScorearr[1] = bestScore;
            DataManager.GetComponent<ControlGameData>().setBestScore(bestScorearr);
            DataManager.GetComponent<ControlGameData>().Save("bestscore");
        }

        playnum = DataManager.GetComponent<ControlGameData>().getPlaynum();
        playnum[1]++;
        DataManager.GetComponent<ControlGameData>().setPlaynum(playnum);
        DataManager.GetComponent<ControlGameData>().Save("playnum");

        DataManager.GetComponent<ControlGameData>().setMoney(money);
        newPuzzle = DataManager.GetComponent<PuzzleManager>().setting_Puzzle(finalScore);

        if (newPuzzle == true)
            PuzzleSign.SetActive(true);
        
        DataManager.GetComponent<ControlGameData>().Save("money");
        DataManager.GetComponent<ControlGameData>().Save("puzzle");

        BestScoreText.text = "Best Score : " + bestScore.ToString();
        ScoreText.text = "Total Score :" + finalScore.ToString();
    }
}
