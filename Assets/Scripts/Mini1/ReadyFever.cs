using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyFever : MonoBehaviour
{

    GameObject GameManager;
    GameObject Player;
    GameObject FeverPlayer;

    Vector3 pos_t = new Vector3(0, 2, 0);
    Vector3 pos_b = new Vector3(0, -2, 0);

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        Player = GameObject.Find("Player");
        FeverPlayer = Player.transform.Find("FeverPlayer").gameObject;
    }

    public void ready()
    {

        GameManager.GetComponent<TimeScore>().StopScore();
        GameManager.GetComponent<MapMove>().enabled = false;
        GameManager.GetComponent<MouseCreate>().enabled = false;
        GameManager.GetComponent<PlanetCreate>().Fever();

        if (Player.transform.position.y > 0)
        {
            Debug.Log("y");
            Player.transform.position = pos_t;
            StartCoroutine(FeverPlayer.GetComponent<FeverMovePlayer>().Move_1());
        }
        else if (Player.transform.position.y < 0)
        {
            Player.transform.position = pos_b;
            StartCoroutine(FeverPlayer.GetComponent<FeverMovePlayer>().Move_2());
        }


    }

}
