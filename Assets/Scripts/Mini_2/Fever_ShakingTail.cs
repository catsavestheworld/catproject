using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever_ShakingTail : MonoBehaviour {

    Sprite[] catfeeling = new Sprite[2];
    string filedir;

    int feelindex;

    public void SettingFeverSprite(string filedir)
    {
        catfeeling[0] = Resources.Load<Sprite>(filedir + "1");
        catfeeling[1] = Resources.Load<Sprite>(filedir + "2");

        gameObject.GetComponent<SpriteRenderer>().sprite = catfeeling[0];
    }

    public void fever_tail()
    {
        StartCoroutine("fever_shakingTail");
    }

    IEnumerator fever_shakingTail()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.45f);
            feelindex = (feelindex + 1) % 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = catfeeling[feelindex];
            //StartCoroutine("shakingTail");
        }
    }
}
