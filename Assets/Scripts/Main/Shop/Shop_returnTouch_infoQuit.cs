using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_returnTouch_infoQuit : MonoBehaviour {

    GameObject MainShop;
    // Use this for initialization
    void Start()
    {
        MainShop = GameObject.Find("Real_Shop");
    }

    private void OnMouseDown()
    {
        MainShop.GetComponent<ShopScript>().offObjinfo();
    }
}
