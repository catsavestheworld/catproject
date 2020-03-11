using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnTouch : MonoBehaviour {

    GameObject mainObj;
	// Use this for initialization
	void Start () {
        mainObj = GameObject.Find("MainObj");
	}

    void OnMouseDown()
    {
        mainObj.GetComponent<SelectMenu>().judgeSelect(gameObject);
        
    }
    
}
