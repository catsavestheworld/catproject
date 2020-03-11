using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_ArrowScript : MonoBehaviour {
    GameObject MainManager;
	// Use this for initialization
	void Start () {
        MainManager = GameObject.Find("MainManager");
	}

    // Update is called once per frame
    /*private void OnMouseOver()
    {
        //Debug.Log(gameObject.name);
    }*/

    private void OnMouseDown()
    {
        MainManager.GetComponent<ChangeCameraPos>().ChangePos();
    }
}
