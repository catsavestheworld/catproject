using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {

    private float speed;

    private static Vector3 localPos;

    void Start()
    {
        speed = 0.12f;
    }
	
    void FixedUpdate()
    {
     
            localPos = gameObject.transform.position;

            transform.Translate(new Vector3(-speed, 0, 0));

            if (localPos.x <= -6.0f) //x가 특정위치에 가면
            {
                 localPos.x += 22.0f;
                 transform.position = localPos; //원래 위치로 돌려준다
                 gameObject.SetActive(false);
            }
        
    }

}
