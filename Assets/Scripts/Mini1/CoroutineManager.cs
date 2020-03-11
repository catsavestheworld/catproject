using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour {

    GameObject GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }


    public void ReStart()
    {
        GameManager.GetComponent<MouseCreate>().MouseOFF();
        StartCoroutine(GameManager.GetComponent<TimeScore>().CountScore());
        StartCoroutine(GameManager.GetComponent<PlanetCreate>().planetON());
        StartCoroutine(GameManager.GetComponent<MouseCreate>().MouseON());
        //GameManager.GetComponent<ReadyFever>().ReCountDown();
        StartCoroutine(GameManager.GetComponent<PlanetSpeed>().SpeedUp());
    }

}
