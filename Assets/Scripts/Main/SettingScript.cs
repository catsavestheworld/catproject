using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScript : CommonJob
{
    GameObject settingBase;
    GameObject AudioManager;

    GameObject categoryObj; //배경음악인지 효과음인지
    GameObject stageObj;//미니게임123,메인화면
    GameObject[] VolumnSize = new GameObject[5];

    Sprite[] volumnSpr = new Sprite[2];
    Sprite[] categorySpr = new Sprite[2];
    Sprite[] stageSpr = new Sprite[4];

    int[][] volumn = new int[4][];// a/10 --> 브금, a%10 --> 효과음, 1이면 브금이고 0이면 효과음임

    int stageIndex; //0,1,2,3
    int categoryIndex;// 0,1 - 0이 배경음악, 1이 효과음

    int i;
    int j;
    // Use this for initialization
    public override void Start()
    {
        base.Start();

        AudioManager = GameObject.Find("AudioManager");
        settingBase = GameObject.Find("Setting_Main");

        categoryObj = GameObject.Find("Setting_Index");
        stageObj = GameObject.Find("Setting_C_Index");

        for (i = 0; i < 5; i++)
            VolumnSize[i] = GameObject.Find("S_Volumn" + i);

        for (i = 0; i < 4; i++)
            volumn[i] = new int[2];

        for (i = 0; i < 2; i++)
        {
            volumnSpr[i] = Resources.Load<Sprite>("Main/SettingSprite/volumn_" + i);
            categorySpr[i] = Resources.Load<Sprite>("Main/SettingSprite/Category_" + i);
        }


        for (i = 0; i < 4; i++)
        {
            if (i == 0)
                stageSpr[i] = Resources.Load<Sprite>("Main/SettingSprite/S_C_Main");
            else
                stageSpr[i] = Resources.Load<Sprite>("Main/SettingSprite/S_C_Mini" + i);
        }

        stageIndex = 0;
        categoryIndex = 0;

        settingBase.SetActive(false);
    }


    public override void initial()
    {
        //데이터를 읽어오고 초기화시키기
        int[] tempvolumn;

        tempvolumn = DataManager.GetComponent<ControlGameData>().getVolumn();

        for (i = 0; i < 4; i++)
        {
            volumn[i][0] = tempvolumn[i] / 10; // 배경음악 볼륨
            volumn[i][1] = tempvolumn[i] % 10; // 효과음 볼륨
        }

        settingBase.SetActive(true);
        stageIndex = 0;
        categoryIndex = 0;

        categoryObj.GetComponent<SpriteRenderer>().sprite = categorySpr[categoryIndex];
        stageObj.GetComponent<SpriteRenderer>().sprite = stageSpr[stageIndex];

        appearVolumn();

    }

    //배경음악인지 효과음인지
    public void changeCategory(string dir)
    {
        if (dir == "Left" && categoryIndex != 0)
            categoryIndex--;
        else if (dir == "Right" && categoryIndex != 1)
            categoryIndex++;

        categoryObj.GetComponent<SpriteRenderer>().sprite = categorySpr[categoryIndex];
        appearVolumn();
    }

    //스테이지는 뭔지
    public void changeStage(string dir)
    {
        if (dir == "Up" && stageIndex != 3)
            stageIndex++;
        else if (dir == "Down" && stageIndex != 0)
            stageIndex--;

        stageObj.GetComponent<SpriteRenderer>().sprite = stageSpr[stageIndex];
        appearVolumn();
    }

    public void changeVolumn(string dir)
    {
        if (dir == "Up" && volumn[stageIndex][categoryIndex] != 9)
        {
            if (volumn[stageIndex][categoryIndex] == 0)
            {
                volumn[stageIndex][categoryIndex] = 5;
            }
            else
                volumn[stageIndex][categoryIndex]++;
        }


        else if (dir == "Down" && volumn[stageIndex][categoryIndex] != 0)
        {
            volumn[stageIndex][categoryIndex]--;
            if (volumn[stageIndex][categoryIndex] == 4)
                volumn[stageIndex][categoryIndex] = 0;
        }
        appearVolumn();
    }

    void appearVolumn()
    {
        int nowvolumn = volumn[stageIndex][categoryIndex] % 5; //--> 0/0,1,2,3,4
        for (i = 0; i < 5; i++)
        {
            if (i <= nowvolumn && volumn[stageIndex][categoryIndex] != 0)
                VolumnSize[i].GetComponent<SpriteRenderer>().sprite = volumnSpr[1];
            else
                VolumnSize[i].GetComponent<SpriteRenderer>().sprite = volumnSpr[0];
        }
    }

    public override void finish()
    {
        base.finish();

        //해당 작업을 끝내기.
        settingBase.SetActive(false);
        save();
        AudioManager.GetComponent<Main_AudioManager>().changeVolumn();
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }

    public override void save()
    {
        //데이터값 저장
        int[] saveVolumn = new int[4];
        for (i = 0; i < 4; i++)
        {
            saveVolumn[i] = volumn[i][0] * 10 + volumn[i][1];
        }


        DataManager.GetComponent<ControlGameData>().setVolumn(saveVolumn);
        DataManager.GetComponent<ControlGameData>().Save("volumn");
    }
}