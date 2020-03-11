using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverManager : MonoBehaviour {

    GameObject[] Monster;
    public IEnumerator Move;
    IEnumerator BackMove;

    GameObject FeverScore;

    Sprite[] monsterBS;
    Sprite[] monsterS;

    void Start()
    {
        Monster = new GameObject[4];
        monsterBS = new Sprite[4];
        monsterS = new Sprite[4];

        FeverScore = GameObject.Find("FeverScore");

        for (int i = 0; i < 4; i++)
        {
            Monster[i] = GameObject.Find("Monster_" + i);
            monsterS[i] = Resources.Load<Sprite>("Monster_" + i);
            monsterBS[i] = Resources.Load<Sprite>("Monster_" + i + "_back");
        }
    }

    

    public void Boss_ON()
    {
        FeverScore.GetComponent<FeverScore>().ReFeverScore();
        int rdnum = Random.Range(0, 4);
        Monster[rdnum].SetActive(true);
        Move = Monster[rdnum].GetComponent<MoveMonster>().Move();
        StartCoroutine(Move);
    }

    public void Monster_back()
    {
        for (int i = 0; i < 4; i++)
        {
            Monster[i].GetComponent<SpriteRenderer>().sprite = monsterBS[i];
            BackMove = Monster[i].GetComponent<FeverMoveMonster>().BackMove();
            StartCoroutine(BackMove);
        }
    }

    public void Monster_normal()
    {
        for(int i=0; i<4; i++)
        {
            Monster[i].GetComponent<SpriteRenderer>().sprite = monsterS[i];
        }
    }
}
