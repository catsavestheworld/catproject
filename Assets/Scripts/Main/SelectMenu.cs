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

    GameObject PlaceObj;

    int selectedIndex;
    // Use this for initialization
    void Start()
    {
        MenuObj = GameObject.Find("Menu");
        mainManager = GameObject.Find("MainManager");
        DataManager = GameObject.Find("DataManager");

        SelectObj = GameObject.Find("Menu_selectImg");

        selectedIndex = 100;

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
        }
        else if (selectedIndex == 5)
        {
            PlaceObj.GetComponent<PlacementScript>().OnCatCollider();
        }

        else if(selectedIndex == -1) //게임 종료
        {
            PlaceObj.GetComponent<PlacementScript>().OnCatCollider();
            DataManager.GetComponent<ControlGameData>().saveforFinish();
            Application.Quit();
        }
        else
        {
            PlaceObj.GetComponent<PlacementScript>().OffCatCollider();
            mainManager.GetComponent<Main_Manager>().doyourJob(selectedIndex);
            MenuObj.SetActive(false);
        }
    }
}
