using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini1_BackToHome : MonoBehaviour
{
    GameObject SceneManager;


    // Use this for initialization
    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");


    }

    // Update is called once per frame
    void OnMouseDown()
    {
        //퍼즐 체크해서 저장
        if (gameObject.name == "Button_home")
        {
            SceneManager.GetComponent<SceneMoving>().BacktoHome();
        }
        else if (gameObject.name == "Button_retry")
            SceneManager.GetComponent<SceneMoving>().PlayAgain("Mini_1");
    }


}
