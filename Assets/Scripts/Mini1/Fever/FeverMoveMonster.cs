using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverMoveMonster : MonoBehaviour {

    private float speed;
    Vector3 pos;

    void Start()
    {
        speed = 0.09f;
    }


    public IEnumerator BackMove()
    {
        
            //Debug.Log("BackMove");
            if (gameObject.transform.position.x >= 17)
            {
                pos = gameObject.transform.position;
                pos.x = 18;
                gameObject.transform.position = pos;
                StopCoroutine("Backmove");
            }
            transform.Translate(new Vector3(+speed, 0, 0));

            yield return new WaitForSeconds(0.01f);

        StartCoroutine("BackMove");
        }
    }

