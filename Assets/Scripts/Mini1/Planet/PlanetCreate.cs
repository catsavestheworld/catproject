using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCreate : MonoBehaviour
{

    //초기위치 상(y축 +2.8정도 )중(0) 하(-2.8)
    private Vector3 mid;
    private Vector3 top;
    private Vector3 bot;

    private static GameObject[] planets;

    GameObject p1;
    GameObject p2 ;
    GameObject p3 ;

    int temp;
    float sum;
    private int rdnum;
    private float seconds;


    void Start()
    {
        mid = new Vector3(18f, 0.1f, 0);
        top = new Vector3(18f, 3f, 0);
        bot = new Vector3(18f, -3f, 0);

        sum = 0;
        seconds = 1.8f;

        p1 = GameObject.Find("Planet_1");
        p2 = GameObject.Find("Planet_4");
        p3 = GameObject.Find("Planet_5");

        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);

        planets = new GameObject[9];
        MakePlanet();
        StartCoroutine("planetON");
    }



    void MakePlanet() //상중하 * 행성 3개 총 9개의 초기위치에 꺼진 오브젝트 미리 생성해놓기 (중복 등장은 안하겠군..)
    {

        for (int i=0; i<9; i++)
        {
            if (i < 3)
                planets[i] = Instantiate(p1, mid, Quaternion.identity) as GameObject; //0,1,2
            else if (i < 6)
                planets[i] = Instantiate(p2, mid, Quaternion.identity) as GameObject; //3,4,5
            else
                planets[i] = Instantiate(p3, mid, Quaternion.identity) as GameObject; //6,7,8

            if (i == 0 || i == 3 || i == 6)
                planets[i].transform.position = top;
            if ( i==1 || i ==4 || i==7)
                planets[i].transform.position = bot;


            planets[i].SetActive(false);
        }

    }

    public IEnumerator planetON()//랜덤으로 행성 하나 켜기
    {
        yield return new WaitForSeconds(seconds);

         //   Debug.Log("PlanetOn called and secondes is " + seconds);

            rdnum = Random.Range(0, 9);
       
        while(rdnum == temp) //전이랑 중복되는 숫자 안하려고...
        {
            rdnum = Random.Range(0, 9);
        }
        temp = rdnum;

            planets[rdnum].SetActive(true);

        if (sum <= 15)
        {
            seconds = 1.45f;
        }
        else if (sum <= 60)
        { 
            if (seconds > 1f)
                seconds -= 0.08f;
            else
                seconds = 0.75f;
        }
        else
            seconds = 0.5f;

        
        sum += seconds;

        StartCoroutine("planetON");
    }

    public void Fever() //피버타임 때 리스트에있는 행성의 fevermove켜줌
    {
        for(int i=0; i<9; i++)
        {
            planets[i].GetComponent <MovePlanet> ().enabled = false;
            if (planets[i].transform.position.y > 0)
                StartCoroutine(planets[i].GetComponent<FeverMovePlanet>().Move_1());
            else
                StartCoroutine(planets[i].GetComponent<FeverMovePlanet>().Move_2());
        }
    }

    public void FeverOff()
    {
        //원래 위치로 옮겨주고 MovePlanet ON
        for (int i = 0; i < 9; i++)
        {
            if (i == 0 || i == 3 || i == 6)
                planets[i].transform.position = top;
            else if (i == 1 || i == 4 || i == 7)
                planets[i].transform.position = bot;
            else
                planets[i].transform.position = mid;

            planets[i].GetComponent<MovePlanet>().enabled = true;
        }
    }
   
    public void PlanetOff()
    {
        for(int i=0; i<9; i++)
        {
            planets[i].SetActive(false);
        }
    }
}
