using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_BackToMain : MonoBehaviour {
    GameObject SceneManager;
    
    // Use this for initialization
    void Start () {
        SceneManager = GameObject.Find("SceneManager");
    }
	
	// Update is called once per frame
	void OnMouseDown () {

        //터치 체크해서 저장
        if (gameObject.name == "BackToMain")
        {
            SceneManager.GetComponent<SceneMoving>().BacktoHome();
        }
        else if (gameObject.name == "TryAgain")
            SceneManager.GetComponent<SceneMoving>().PlayAgain("Mini_2");
    }

}
