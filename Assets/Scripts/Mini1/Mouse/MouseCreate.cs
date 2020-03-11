using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCreate : MonoBehaviour {

    GameObject Mouse;

    private static int create_time;
    Vector3[] rdpos;

    void Start()
    {
        create_time = 10;
        rdpos = new Vector3[5];


        //쥐 한마리 생성해놓고 꺼놓기
        Mouse = GameObject.Find("SpaceMouse");
        Mouse.SetActive(false);

        rdpos[0] = new Vector3(16f, 0, -5);
        rdpos[1] = new Vector3(16f, 1.5f, -5);
        rdpos[2] = new Vector3(16f, 3.5f, -5);
        rdpos[3] = new Vector3(16f, -1.5f, -5);
        rdpos[4] = new Vector3(16f, -3.5f, -5);

        Mouse.SetActive(false);
        StartCoroutine("MouseON");
    }

    public IEnumerator MouseON() // 5~20초마다 랜덤으로 상/중/하 위치에서 켜기

    {
        
            yield return new WaitForSeconds(create_time);

            Mouse.transform.position = rdpos[Random.Range(0, 5)];
            Mouse.SetActive(true);
            create_time = Random.Range(5, 21);
            Debug.Log(create_time);
        if (gameObject.activeInHierarchy)
            StartCoroutine("MouseON");
    }

    public void MouseOFF()
    {
        Mouse.transform.position = rdpos[Random.Range(0, 5)];
        Mouse.SetActive(false);
    }
}
