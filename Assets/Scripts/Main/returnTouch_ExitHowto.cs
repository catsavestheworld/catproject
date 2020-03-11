using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnTouch_ExitHowto : MonoBehaviour {
    GameObject MiniGameManager;

    // Use this for initialization
    void Start()
    {
        MiniGameManager = GameObject.Find("Real_MiniGame");
    }

    void OnMouseDown()
    {
        MiniGameManager.GetComponent<MiniGameScript>().offHowto();
    }
}
