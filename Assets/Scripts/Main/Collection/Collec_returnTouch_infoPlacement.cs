using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collec_returnTouch_infoPlacement : MonoBehaviour {

    GameObject CollecManager;
    int index;
    string sprname;

    // Use this for initialization
    void Start()
    {
        CollecManager = GameObject.Find("Real_Collection");
    }

    private void OnMouseDown()
    {
        sprname = gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite.name;
        index = int.Parse(sprname.Substring(sprname.Length - 1));
        Debug.Log(index);
        CollecManager.GetComponent<CollectionScript>().placeObj(index);
    }
}
