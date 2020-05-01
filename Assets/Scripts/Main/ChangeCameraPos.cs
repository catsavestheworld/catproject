using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraPos : MonoBehaviour
{

    public GameObject Story;

    GameObject[] Arrow = new GameObject[2];
    GameObject Camera;
    GameObject DataManager;
    GameObject CatObj;
    GameObject MenuObj;
    GameObject[] MainObj = new GameObject[4];

    Button ArrowUI;

    int[] furniture = new int[9];

    // Use this for initialization
    void Start()
    {
        Arrow[0] = GameObject.Find("Arrow_right");
        Arrow[1] = GameObject.Find("Arrow_left");
        Camera = GameObject.Find("MainCamera");
        MenuObj = GameObject.Find("Menu");
        DataManager = GameObject.Find("DataManager");
        CatObj = GameObject.Find("Cat");
        MainObj[0] = GameObject.Find("Shop");
        MainObj[1] = GameObject.Find("Setting");
        MainObj[2] = GameObject.Find("MiniGame");
        MainObj[3] = GameObject.Find("Collection");

        judgeLocked();
    }

    public void turnOnObj()
    {
        Arrow[0].SetActive(true);
        Arrow[1].SetActive(true);

    }
    public void turnOffObj()
    {
        Arrow[0].SetActive(false);
        Arrow[1].SetActive(false);
    }

    public void ChangePos()
    {
        if (Camera.transform.localPosition.x < 10)
        {
            Camera.transform.Translate(+21f, 0, 0);
            MenuObj.transform.Translate(+21f, 0, 0);
            for(int i=0;i<MainObj.Length;i++){
                MainObj[i].transform.Translate(+21f, 0, 0);
            }
            Story.transform.Translate(+21f, 0, 0);
        }
        else
        {
            Camera.transform.Translate(-21f, 0, 0);
            MenuObj.transform.Translate(-21f, 0, 0);
            for(int i=0;i<MainObj.Length;i++){
                MainObj[i].transform.Translate(-21f, 0, 0);
            }
            Story.transform.Translate(-21f, 0, 0);
        }

        CatObj.GetComponent<Cat_interact>().CatVolSetting();
    }

    void judgeLocked()
    {
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();
        for (int j = 0; j < 4; j++)
        {
            if (furniture[j] == -1){
                //하나라도 앞 네개를 구매하지 않았으면 lock되어있는 상황임
                turnOffObj();
                return;
            }
        }
        // unlock
        turnOnObj();
    }

    public bool returnLocked()
    {
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();
        for (int j = 0; j < 4; j++)
        {
            if (furniture[j] == -1)
                return false;//하나라도 앞 네개 중 구매 안 한 것 있으면 locked 상태임
        }
        return true;
    }
}
