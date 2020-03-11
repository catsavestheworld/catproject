using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_returnTouch_Category : MonoBehaviour {

    GameObject MainShop;
    // Use this for initialization
    void Start()
    {
        //Debug.Log("category start");
        MainShop = GameObject.Find("Real_Shop");
    }

    private void OnMouseDown()
    {
        //Debug.Log(MainShop.name);
        MainShop.GetComponent<ShopScript>().getCategorybutton(gameObject.name);
    }
}
