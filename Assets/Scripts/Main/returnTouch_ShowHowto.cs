using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnTouch_ShowHowto : MonoBehaviour {
    GameObject MiniGameManager;

	// Use this for initialization
	void Start () {
        MiniGameManager = GameObject.Find("Real_MiniGame");
	}
	
	void OnMouseDown()
    {
        int myNum = int.Parse(gameObject.name.Substring(0, 1));
        Debug.Log("MYnum is "+myNum);
        MiniGameManager.GetComponent<MiniGameScript>().showHowto(myNum);
    }
}
