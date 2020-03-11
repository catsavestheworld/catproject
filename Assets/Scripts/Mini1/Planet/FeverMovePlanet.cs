using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverMovePlanet : MonoBehaviour {

    /* 피버타임 시작 전 행성 위치를 화면밖으로 이동시켜주는 스크립트 */
    private float speed;

    private Vector3 mid;
    private Vector3 top;
    private Vector3 bot;

    static int check; //0=mid , 1 = top, -1 = mid
	
    void Start()
    {
        check = 0;
        speed = 0.07f;
        mid = new Vector3(18f, 0.1f, 0);
        top = new Vector3(18f, 3f, 0);
        bot = new Vector3(18f, -3f, 0);
    }

    void Check_pos()
    { 
        if (gameObject.transform.position.y == top.y)
            check++;
        else if (gameObject.transform.position.y == bot.y)
            check--;
    }

	public IEnumerator Move_1() //위로 이동시켜줌
    {
        Check_pos();

        while (true)
        {
            if ( transform.position.y >= 7.5)
            {
                //초기 위치로 보내주기 
                if (check == 0)
                    gameObject.transform.position = mid;
                  
                else 
                    gameObject.transform.position = top;
                    
                gameObject.SetActive(false);
            }
            transform.Translate(new Vector3(0, speed, 0));
            yield return new WaitForSeconds(0.02f);
        }
    }

    public IEnumerator Move_2() //아래로 이동시켜줌
    {
        Check_pos();
        while (true)
        {
            if (transform.position.y <= -7.5)
            {
                //초기 위치로 보내주기 
                gameObject.transform.position = bot;
                gameObject.SetActive(false);
            }
            transform.Translate(new Vector3(0, -speed, 0));
            yield return new WaitForSeconds(0.02f);
        }
    }
}
