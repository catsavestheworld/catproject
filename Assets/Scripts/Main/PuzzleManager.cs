using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{

    //퍼즐을 얻을 수 있는 각각의 조건들
    int[] ScorePuzzle = new int[8];

    int i;

    int havingPuzzle;

    int sceneIndex;

    int[] totalplaynum;
    int myplaynum;

    int[] datapuzzle = new int[6];
    int[] mypuzzle = new int[2];
    int[] tempPuzzle = new int[8];

    bool[] getPuzzle = new bool[2];

    List<int> puzzleList;
    void Start()
    {
        ScorePuzzle[0] = 100;
        ScorePuzzle[1] = 500;
        ScorePuzzle[2] = 700;
        ScorePuzzle[3] = 1000;
        ScorePuzzle[4] = 1200;
        ScorePuzzle[5] = 1500;
        ScorePuzzle[6] = 2000;
        ScorePuzzle[7] = 2500;
    }

    public bool setting_Puzzle(int myscore)
    {
        getPuzzle[0] = false;
        getPuzzle[1] = false;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                break;
            case "Mini_1":
                sceneIndex = 0;
                break;
            case "Mini_2":
                sceneIndex = 2;
                break;
            case "Mini_3":
                sceneIndex = 4;
                break;
        }

        datapuzzle = gameObject.GetComponent<ControlGameData>().getPuzzle();
        totalplaynum = gameObject.GetComponent<ControlGameData>().getPlaynum();

        mypuzzle[0] = datapuzzle[sceneIndex]; //플레이 횟수로 인한 퍼즐
        mypuzzle[1] = datapuzzle[sceneIndex + 1]; //스코어를 통한 펴즐
        myplaynum = totalplaynum[sceneIndex / 2];

        if (myscore > 300)
        {
            getPuzzle[0] = getPuzzle_Playnum();

        }
        getPuzzle[1] = getPuzzle_Score(myscore);

        gameObject.GetComponent<ControlGameData>().setPuzzle(datapuzzle);
        gameObject.GetComponent<ControlGameData>().Save("puzzle");

        if (getPuzzle[0] == true || getPuzzle[1] == true)
            return true;
        else
            return false;
    }

    //플레이횟수 따른 퍼즐획득
    bool getPuzzle_Playnum(){
        bool returnbool = false;
        System.Array.Clear(tempPuzzle, 0, tempPuzzle.Length);
        puzzleList = new List<int>();
        havingPuzzle = 0;

        for (i = 0; i < 8; i++) {
            tempPuzzle[i] = mypuzzle[0] % 2;
            mypuzzle[0] /= 2;

            if (tempPuzzle[i] == 0) //아직 퍼즐이 없는 조각이라면
            {
                puzzleList.Add(i); //리스트에 해당 인덱스 추가 추가
            }
            else //조각이 있는 퍼즐이면 퍼즐 개수 추가
                havingPuzzle++;
        }

        if (havingPuzzle != 8){
            //퍼즐을 얻었다!
            if ((myplaynum / 5) + 1 > havingPuzzle){
                int pIndex = Random.Range(0, puzzleList.Count);
                tempPuzzle[puzzleList[pIndex]] = 1;
                returnbool = true;
            }

            int returnval = 0;
            int exp = 1;

            for (i = 0; i < 8; i++)
            {
                returnval = tempPuzzle[i] * exp + returnval;
                exp *= 2;
            }

            datapuzzle[sceneIndex] = returnval;
            
        }
        
        return returnbool;

    }

    //점수에 따른 퍼즐획득
    bool getPuzzle_Score(int myscore)
    {
        System.Array.Clear(tempPuzzle, 0, tempPuzzle.Length);
        puzzleList = new List<int>();
        havingPuzzle = 0;

        for (i = 0; i < 8; i++)
        {
            tempPuzzle[i] = mypuzzle[1] % 2;
            mypuzzle[1] /= 2;

            if (tempPuzzle[i] == 0) //아직 퍼즐이 없는 조각이라면
            {
                puzzleList.Add(i); //리스트에 해당 인덱스 추가 추가
            }
            else //조각이 있는 퍼즐이면 퍼즐 개수 추가
                havingPuzzle++;
        }

        //퍼즐을 얻을 수 있다
        if (havingPuzzle != 8)
        {
            //얻을 수 있는 상황이라면
            if (myscore > ScorePuzzle[havingPuzzle])
            {
                int pIndex = Random.Range(0, puzzleList.Count);
                tempPuzzle[puzzleList[pIndex]] = 1;


                int returnval = 0;
                int exp = 1;

                for (i = 0; i < 8; i++)
                {
                    returnval = tempPuzzle[i] * exp + returnval;
                    exp *= 2;
                }

                datapuzzle[sceneIndex + 1] = returnval;

                return true;

            }
            
        }
        return false;
    }

}
