using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collec_returnTouch_ExitButton : MonoBehaviour {

    GameObject CollecManager;

	// Use this for initialization
	void Start () {
        CollecManager = GameObject.Find("Real_Collection");
	}

    private void OnMouseDown()
    {
        CollecManager.GetComponent<CollectionScript>().finish();
    }
}
