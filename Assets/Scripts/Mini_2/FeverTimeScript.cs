using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTimeScript : MonoBehaviour
{
    public int touchnum;
    GameObject toy;
    Text FeverText;

    GameObject Prism;

    Vector3 DownScale;
    Vector3 UpScale;

    GameObject AudioManager;
    AudioClip fever_toySwing;
    Vector3 volVector;
    float effectvolume;

    void Start()
    {
        toy = GameObject.Find("FeverTime").transform.Find("Item").gameObject;
        Prism = toy.transform.Find("Prism").gameObject;
        Prism.SetActive(false);

        FeverText = GameObject.Find("FeverText").GetComponent<Text>();

        DownScale = new Vector3(0.7f, 0.7f, 1);
        UpScale = new Vector3(1, 1, 1);

        AudioManager = GameObject.Find("AudioManager");
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        fever_toySwing = AudioManager.GetComponent<Main_AudioManager>().fever_toySwing;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;
    }

    void OnMouseDown()
    {
        if (effectvolume != 0)
        {
            AudioSource.PlayClipAtPoint(fever_toySwing, volVector);

        }
        Prism.SetActive(true);
        StartCoroutine("sparkling");
        toy.transform.eulerAngles += new Vector3(0, 180, 0);
        //StartCoroutine("sparkling");
        touchnum++;

        FeverText.transform.localScale = DownScale;
        FeverText.text = touchnum.ToString() + " HIT!";
    }

    private void OnMouseUp()
    {
        FeverText.transform.localScale = UpScale;
    }

    IEnumerator sparkling()
    {
        Prism.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        yield return new WaitForSeconds(0.2f);
        Prism.transform.localScale = new Vector3(0.8f, 0.6f, 1);
        Prism.SetActive(false);
    }
}
