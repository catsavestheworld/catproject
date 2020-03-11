using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour {

    public Text time_score;
    public int time;
    static int temp;

    // Use this for initialization
    void Start () {
        Debug.Log("start in timescore");
        temp = 0;
        time = 0;
    }
	
    public void BScore()
    {
        time_score.text = " : " + time.ToString();
    }

	public IEnumerator CountScore()
    {
            time_score.text = " : "+ time.ToString();
            yield return new WaitForSeconds(0.1f);
            time++;
            StartCoroutine("CountScore");
    }

    public void StopScore()
    {
        StopCoroutine("CountScore");
        temp = time;
        time_score.text = " : " + temp.ToString();
    }
}
