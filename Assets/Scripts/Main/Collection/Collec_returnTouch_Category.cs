using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collec_returnTouch_Category : MonoBehaviour {

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
        //Debug.Log("checked");
        sprname = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        sprname = sprname.Substring(sprname.Length - 1);
        if (sprname == "C")
            index = 0;
        else if (sprname == "F")
            index = 1;
        else if (sprname == "S")
            index = 2;
        else if (sprname == "P")
            index = 3;

        CollecManager.GetComponent<CollectionScript>().SettingCategory(index);
    }
}
