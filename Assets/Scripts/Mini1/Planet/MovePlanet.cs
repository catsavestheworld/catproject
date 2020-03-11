using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlanet : MonoBehaviour
{
    GameObject GameManager;
    float speed;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }


    void FixedUpdate()
    {
        Vector3 localPos = gameObject.transform.position;
        speed = GameManager.GetComponent<PlanetSpeed>().speed;

        transform.Translate(new Vector3(-speed, 0, 0)); //speed마다 왼쪽으로 이동

        if (localPos.x <= -8.0f) //x가 특정위치에 가면
        {
            localPos.x += 26f;
            transform.position = localPos; //원래 위치로 돌려준다
            gameObject.SetActive(false);
        }

    }
    
}