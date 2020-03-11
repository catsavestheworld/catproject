using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    GameObject Overimage;
    GameObject background;
    GameObject Button1;

	void Start () {
        Overimage = GameObject.Find("GameOver_Space");
        background = GameObject.Find("background_black");
        Button1 = GameObject.Find("PlayAgain_space");

        Overimage.SetActive(false);
        background.SetActive(false);
        Button1.SetActive(false);
	}
	
}
