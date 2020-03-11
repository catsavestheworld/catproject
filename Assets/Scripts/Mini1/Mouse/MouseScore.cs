using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseScore : MonoBehaviour {

    public Text mouse_score;

    private Vector3 localpos;
    private static int count;

	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
       
            Debug.Log("쥐 클릭인식");
            count++;
            localpos = gameObject.transform.position;
            localpos.x = 16f;
            transform.position = localpos;
            gameObject.SetActive(false);

            mouse_score.text = "쥐돌이 : " + count.ToString();
       
    }
}
