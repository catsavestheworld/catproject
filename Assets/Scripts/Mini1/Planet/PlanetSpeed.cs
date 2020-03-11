using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpeed : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        speed = 0.24f;
        StartCoroutine("SpeedUp");
	}

    public IEnumerator SpeedUp()
    {

        yield return new WaitForSeconds(10);

        if (speed > 1.8f)
        {
            speed = 2.0f;
            StopCoroutine("SpeedUp");
        }
        speed += 0.05f;

    }
}
