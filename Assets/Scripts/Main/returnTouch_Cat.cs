using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnTouch_Cat : MonoBehaviour {
    GameObject CatParent;

    // Use this for initialization
    void Start()
    {
        CatParent = GameObject.Find("Cat");
    }

    private void OnMouseDown()
    {
        string[] info = gameObject.GetComponent<SpriteRenderer>().sprite.name.Split('_');
        Debug.Log(info.Length);
        CatParent.GetComponent<Cat_interact>().getreaction_Cat(gameObject, int.Parse(info[1]), int.Parse(info[2]));
    }
}
