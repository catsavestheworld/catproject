using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnTouch_HowtoArrow : MonoBehaviour {
    GameObject MiniGameManager;

    // Use this for initialization
    void Start()
    {
        MiniGameManager = GameObject.Find("Real_MiniGame");
    }

    void OnMouseDown()
    {
        string dir = gameObject.name.Substring(6);
        MiniGameManager.GetComponent<MiniGameScript>().showAnotherspr(dir);
    }
}
