using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collec_skipStory : MonoBehaviour {
    GameObject IntroStory;
	// Use this for initialization
	void Start () {
        IntroStory = GameObject.Find("Intro_Story");
	}

    private void OnMouseDown()
    {
        IntroStory.GetComponent<ShowingIntro>().skipStory();
    }
}
