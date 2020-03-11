using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_returnTouch : MonoBehaviour {

    GameObject GameManager;
    string Objname;
	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("Real_MiniGame");
        Objname = gameObject.name.Substring(9);
	}
	

    private void OnMouseDown()
    {
        if (Objname.Equals("goout"))
            GameManager.GetComponent<MiniGameScript>().goout();
        else
        {
            Debug.Log("scene index is "+(int.Parse(Objname.Substring(0, 1)) - 1));
            GameManager.GetComponent<MiniGameScript>().playGame(int.Parse(Objname.Substring(0, 1))-1);
        }
    }
}
