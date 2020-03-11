using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClick_Cat : MonoBehaviour {

    GameObject cat;
	// Use this for initialization
	void Start () {
        cat = gameObject.transform.parent.gameObject;
	}

    private void OnMouseDown()
    {
        cat.GetComponent<DemandManager>().isRightAct();
    }
}
