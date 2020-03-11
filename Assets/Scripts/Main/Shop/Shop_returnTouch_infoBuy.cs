using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_returnTouch_infoBuy : MonoBehaviour {

    GameObject MainShop;
    // Use this for initialization
    void Start()
    {
        MainShop = GameObject.Find("Real_Shop");
    }

    private void OnMouseDown()
    {
        MainShop.GetComponent<ShopScript>().buyObj(gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite.name);
    }
}
