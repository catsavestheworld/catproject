using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraPos : MonoBehaviour {

    GameObject[] Arrow = new GameObject[2];
    GameObject Camera;
    GameObject DataManager;
    GameObject CatObj;
    //GameObject AudioManager;
    //GameObject CameraArrow;

    Button ArrowUI;

    int[] furniture = new int[8];

    // Use this for initialization
    void Start () {
        Arrow[0] = GameObject.Find("Arrow_right");
        Arrow[1] = GameObject.Find("Arrow_left");
        Camera = GameObject.Find("MainCamera");
        //CameraArrow = GameObject.Find("CameraArrow");
        //ArrowUI = GameObject.Find("CameraArrow").GetComponent<Button>();
        DataManager = GameObject.Find("DataManager");
        CatObj = GameObject.Find("Cat");

        judgeLocked();
	}

    public void turnOnObj()
    { 
        //CameraArrow.SetActive(true);
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
        //Debug.Log("called");
        if (Camera.transform.localPosition.x < 10)
        {
            //CameraArrow.GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));
			Camera.transform.Translate(+21, 0, 0);
            CatObj.GetComponent<Cat_interact>().CatVolSetting();

        }
        else
        {
            //CameraArrow.GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));
            Camera.transform.Translate(-21f, 0, 0);
            CatObj.GetComponent<Cat_interact>().CatVolSetting();
        }
        
    }

    void judgeLocked()
    {
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();
        bool unlockcondition = true; // 일단 언록되어있다고 가정되고
        for (int j = 0; j < 4; j++)
        {
            if (furniture[j] == -1)
                unlockcondition = false;//하나라도 앞 네개 중 구매 안 한 것 있으면 언록시키기
            //Debug.Log(unlockcondition);
        }
        //Debug.Log(unlockcondition);
        if (unlockcondition == true)
        {
            //Debug.Log("True!");
            turnOnObj();
        }
        else
            turnOffObj();
    }

    public bool returnLocked()
    {
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();
        bool unlockcondition = true; // 일단 언록되어있다고 가정되고
        for (int j = 0; j < 4; j++)
        {
            if (furniture[j] == -1)
                unlockcondition = false;//하나라도 앞 네개 중 구매 안 한 것 있으면 언록시키기
        }
        return unlockcondition;
    }
}
