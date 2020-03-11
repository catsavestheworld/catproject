using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : MonoBehaviour {

    private float speed;
    Vector3 pos;

    void Start()
    {
        speed = 0.1f;
    }

	public IEnumerator Move()
    {
            //Debug.Log("Move");
            if (gameObject.transform.position.x <= 10)
            {
                pos = gameObject.transform.position;
                pos.x = 10;
                gameObject.transform.position = pos;
                StopCoroutine("Move");
            }
            transform.Translate(new Vector3(-speed, 0, 0));

            yield return new WaitForSeconds(0.01f);

        StartCoroutine("Move");
        }
    }

