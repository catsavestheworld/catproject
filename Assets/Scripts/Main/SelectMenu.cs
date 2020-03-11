using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    //일단 메뉴를 누르면 콜라이더 다 끄고 + 선택지가 뜨고 
    //나가기면 게임 끝내기
    //아니면 각자를 켜고+선택지는 끄고
    //돌아가기 버튼이면 콜라이더 다 켜고 + 선택지 끄고

    GameObject MenuObj;
    GameObject mainManager;
    GameObject DataManager;
    GameObject SelectObj;
    GameObject GooutSelect;

    GameObject PlaceObj;

    //real_Object
    //GameObject[] furnitureObj = new GameObject[4];
    //GameObject[] furnitureText = new GameObject[4];
    //Sprite[][] furnitureSpr = new Sprite[4][];
    //bool[] selectedBool = new bool[4];

    int selectedIndex;
    //int beforeselected;

    //string sprdir = "Main/";
    //string tempdir;

    // Use this for initialization
    void Start()
    {
        MenuObj = GameObject.Find("Menu");
        mainManager = GameObject.Find("MainManager");
        DataManager = GameObject.Find("DataManager");

        SelectObj = GameObject.Find("Menu_selectImg");
        GooutSelect = GameObject.Find("Menu_goout");

        SelectObj.SetActive(false);
        GooutSelect.SetActive(false);

        selectedIndex = 100;
        //beforeselected = -1;

        /*
        for (int i = 0; i < 4; i++)
        {
            //furnitureSpr[i] = new Sprite[2];

            switch (i)
            {
                case 0:
                    furnitureObj[0] = GameObject.Find("Real_Shop");
                    //furnitureText[0] = GameObject.Find("Shop_text");
                    tempdir = sprdir + "door";
                    break;
                case 1:
                    tempdir = sprdir + "food";
                    furnitureObj[1] = GameObject.Find("Real_Collection");
                    //furnitureText[1] = GameObject.Find("Collection_text");
                    break;
                case 2:
                    tempdir = sprdir + "toy";
                    furnitureObj[2] = GameObject.Find("Real_MiniGame");
                    //furnitureText[2] = GameObject.Find("MiniGame_text");
                    break;
                case 3:
                    furnitureObj[3] = GameObject.Find("Real_Setting");
                    //furnitureText[3] = GameObject.Find("Setting_text");
                    tempdir = sprdir + "window";
                    break;
            }
            //furnitureText[i].SetActive(false);

            //furnitureSpr[i][0] = Resources.Load<Sprite>(tempdir);
            //furnitureSpr[i][1] = Resources.Load<Sprite>(tempdir + "_selected");
        }
        */

        PlaceObj = GameObject.Find("Placement");

        MenuObj.SetActive(true);
    }

    //내가 터치한 게 어떤 애인지 체크
    public void judgeSelect(GameObject selectedObj)
    {

        switch (selectedObj.name)
        {
            case "Menu_goout":
                selectedIndex = 5;
                break;
            case "Menu":
                selectedIndex = 4;
                break;
            case "Select_ExitGame":
                selectedIndex = -1;
                break;
            case "Select_Shop":
                selectedIndex = 0;
                break;
            case "Select_Setting":
                selectedIndex = 3;
                break;
            case "Select_MiniGame":
                selectedIndex = 2;
                break;
            case "Select_Collection":
                selectedIndex = 1;
                break;
        }

        if (selectedIndex == 4)
        {
            PlaceObj.GetComponent<PlacementScript>().OffCatCollider();
            SelectObj.SetActive(true);
            GooutSelect.SetActive(true);
            MenuObj.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (selectedIndex == 5)
        {
            PlaceObj.GetComponent<PlacementScript>().OnCatCollider();
            SelectObj.SetActive(false);
            GooutSelect.SetActive(false);
            MenuObj.GetComponent<BoxCollider2D>().enabled = true;
        }

        else if(selectedIndex == -1) //게임 종료
        {
            PlaceObj.GetComponent<PlacementScript>().OnCatCollider();
            MenuObj.GetComponent<BoxCollider2D>().enabled = true;
            DataManager.GetComponent<ControlGameData>().saveforFinish();
            Application.Quit();
        }
        else
        {
            PlaceObj.GetComponent<PlacementScript>().OffCatCollider();
            SelectObj.SetActive(false);
            GooutSelect.SetActive(false);
            mainManager.GetComponent<Main_Manager>().doyourJob(selectedIndex);
            MenuObj.GetComponent<BoxCollider2D>().enabled = true;
            MenuObj.SetActive(false);
        }
            


        /*
        //선택한 걸 다시 한 번 선택한 경우
        if (beforeselected == selectedIndex && beforeselected != -1)
        {
            //Debug.Log("selected again!" + beforeselected + "beforeselected is ");
            mainManager.GetComponent<Main_Manager>().doyourJob(selectedIndex);

            selectedIndex = -1;
            beforeselected = -1;
        }
        else // 아니라면
        {
            //이전 선택을 업그레이드시켜줌
            beforeselected = selectedIndex;
            for (int i = 0; i < 4; i++)
            {
                if (i == beforeselected) // 선택한 것의 경우
                {
                    selectedBool[i] = true;
                    //furnitureObj[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i][1];
                    //furnitureText[i].SetActive(true);
                }
                else
                {
                    selectedBool[i] = false;
                    //furnitureObj[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i][0];
                    //furnitureText[i].SetActive(false);
                }
            }
        }*/
    }
}
