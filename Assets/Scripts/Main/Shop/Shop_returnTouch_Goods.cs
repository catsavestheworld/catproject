using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_returnTouch_Goods : MonoBehaviour {

    GameObject MainShop;
    // Use this for initialization
    void Start()
    {
        MainShop = GameObject.Find("Real_Shop");
    }

    private void OnMouseDown()
    {
        MainShop.GetComponent<ShopScript>().showObjinfo(gameObject.GetComponent<SpriteRenderer>().sprite.name);
    }
}
