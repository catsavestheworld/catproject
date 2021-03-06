﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collec_returnTouch_Goods : MonoBehaviour {

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
        sprname = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        if(sprname == "willbeUpdated" || sprname == "Collection_Goods_Nothing"){
            return;
        }
        index = int.Parse(sprname.Substring(sprname.Length - 1));
        CollecManager.GetComponent<CollectionScript>().showInfo(index);
    }
}
