using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main_Manager : MonoBehaviour
{
    GameObject MenuObj;
    GameObject AudioManager;
    GameObject DataManager;
    
    GameObject[] realFurObj = new GameObject[4];

    int i;
    int nowactiveIndex;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;

        MenuObj = GameObject.Find("Menu");
        AudioManager = GameObject.Find("AudioManager");

        realFurObj[0] = GameObject.Find("Real_Shop");
        realFurObj[1] = GameObject.Find("Real_Collection");
        realFurObj[2] = GameObject.Find("Real_MiniGame");
        realFurObj[3] = GameObject.Find("Real_Setting");

        AudioManager.GetComponent<Main_AudioManager>().setting();

        nowactiveIndex = -1;
    }

    //콜라이더가 붙어있는 기존 오브젝트들의 콜라이더를 꺼 주고
    //해당 오브젝트를 켜 준다.
    //initial 작업 수행
    public void doyourJob(int objindex)
    {
        turnOffCollider();
        nowactiveIndex = objindex;
        realFurObj[objindex].SetActive(true);
        realFurObj[objindex].GetComponent<CommonJob>().initial();

    }

    //켜져있던 오브젝트를 끄고, 콜라이더를 다 켜준다.
    public void backtoMain()
    {
        MenuObj.SetActive(true);
        turnOnCollider();
        realFurObj[nowactiveIndex].SetActive(false);
        nowactiveIndex = -1;
    }

    //각각의 오브젝트의 콜라이더를 켜 준다
    void turnOnCollider()
    {
        if (gameObject.GetComponent<ChangeCameraPos>().returnLocked() == true)
        {

            gameObject.GetComponent<ChangeCameraPos>().turnOnObj();
        }
        
    }

    //각각의 오브젝트의 콜라이더를 꺼 준다
    void turnOffCollider()
    {
        if (gameObject.GetComponent<ChangeCameraPos>().returnLocked() == true)
        {

            gameObject.GetComponent<ChangeCameraPos>().turnOffObj();
        }
    }

    void objsetactiveFalse(GameObject[] obj)
    {
        for (i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
        }
    }
}
