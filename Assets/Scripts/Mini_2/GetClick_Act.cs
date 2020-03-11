using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClick_Act : MonoBehaviour {

    GameObject ActParent;

    public Sprite[] myActspr = new Sprite[2];
    string filedir = "";

	// Use this for initialization
	void Start () {
        ActParent = gameObject.transform.parent.gameObject;

        switch (gameObject.name)
        {
            case "Touch":
                filedir = "Pic_3/button_brush";
                break;
            case "Eat":
                filedir = "Pic_3/button_can";
                break;
            case "Play":
                filedir = "Pic_3/button_mouse";
                break;

        }
        myActspr[0] = Resources.Load<Sprite>(filedir);
        myActspr[1] = Resources.Load<Sprite>(filedir + "_selected");
    }


    private void OnMouseDown()
    {
        ActParent.GetComponent<ActManager>().JudgeAct(gameObject);
        
    }
}
