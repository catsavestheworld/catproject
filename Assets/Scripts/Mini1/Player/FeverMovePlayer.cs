using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverMovePlayer : MonoBehaviour {

    /* 피버타임 시작 전 플레이어 위치를 (0,0,0)으로 이동시켜주는 스크립트 */
    GameObject Player;

    private float speed;
    Vector3 pos;

    void Start()
    {
        Player = GameObject.Find("Player");
        pos = new Vector3(0, 0, 0);
        speed = 0.075f;
    }

    public IEnumerator Move_1() //플레이어가 화면 위쪽에 있을 때 실행되는 이동문
    {
       
            if (Player.transform.position.y <= 0)
            {
                Player.transform.position = pos;
            StopCoroutine("Move_1");
                
            }
            Player.transform.Translate(new Vector3(0, -speed, 0));
            yield return new WaitForSeconds(0.03f);

        StartCoroutine("Move_1");
    }

    public IEnumerator Move_2() //플레이어가 화면 아래쪽에 있을 때 실행되는 이동문
    {

            if (Player.transform.position.y >= 0)
            {
                Player.transform.position = pos;

                StopCoroutine("Move_2");
            }
            Player.transform.Translate(new Vector3(0, speed, 0));
            yield return new WaitForSeconds(0.03f);

        StartCoroutine("Move_2");
    }
}
