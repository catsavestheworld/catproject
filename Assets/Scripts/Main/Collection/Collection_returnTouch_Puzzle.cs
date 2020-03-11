using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_returnTouch_Puzzle : MonoBehaviour {

    GameObject CollecManager;
    string Objname;
    // Use this for initialization
    void Start()
    {
        CollecManager = GameObject.Find("Real_Collection");
    }

    private void OnMouseDown()
    {
        Objname = gameObject.name;
        CollecManager.GetComponent<CollectionScript>().puzzleArrow(Objname.Substring(9));
    }

}
