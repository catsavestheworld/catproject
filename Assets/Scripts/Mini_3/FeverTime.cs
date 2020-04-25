using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTime : MonoBehaviour
{
    //for audio effect
    GameObject AudioManager;
    AudioClip PresentClicking;
    Vector3 volVector;
    float effectvolume;

	GameObject presentBomb;

    public int presentAdd;
    public bool fevercall;
    Text presentText;

    // Use this for initialization
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        PresentClicking = AudioManager.GetComponent<Main_AudioManager>().PresentClicking;
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

		presentBomb = GameObject.Find ("fever_box_bling");
        presentText = GameObject.Find("Manager").GetComponent<UIManager>().feverHit;

		presentBomb.SetActive (false);
        fevercall = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (gameObject.GetComponent<Transform>().position.x < 1.45f)
        {
            gameObject.GetComponent<Transform>().position += new Vector3(0.055f, 0, 0);
			presentBomb.GetComponent<Transform>().position += new Vector3(0.055f, 0, 0);
        }
    }

    //setting where the present will come out when fever time starts
    public void SettingPos()
    {
        presentBomb.SetActive(false);
        gameObject.transform.localPosition = new Vector3(-10.55f, 0.98f, 1);
		presentBomb.transform.localPosition = new Vector3(-10.55f, 0.98f, 1);
    }

	IEnumerator BombOn()
	{
		presentBomb.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		presentBomb.SetActive (false);
	}

    // showing 00HIT when present is pressed 
    void OnMouseDown()
    {
        if (gameObject.GetComponent<Transform>().position.x < 2.35f)
        {
            presentAdd++;
            GameObject.Find("Manager").GetComponent<UIManager>().TextPlus();
            StartCoroutine("SizeControl");
			StartCoroutine ("BombOn");
            //PresentClickingAudio@@@@
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(PresentClicking, volVector);
        }        
    }

    IEnumerator SizeControl()
    {
        gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0);
        presentText.transform.localScale += new Vector3(-0.1f, -0.1f, 0);
        yield return new WaitForSeconds(0.04f);
        gameObject.transform.localScale += new Vector3(-0.3f, -0.3f, 0);
        presentText.transform.localScale += new Vector3(0.1f, 0.1f, 0);
    }

    //used to expand the time in fever
    IEnumerator TimeExpand()
    {
        int feverPlayTime = GameObject.Find("Main Camera").GetComponent<TotalManager_3>().feverPlayTime;
        yield return new WaitForSeconds(1.4f+feverPlayTime);
        FeverBonus();
		GameObject.Find("Main Camera").GetComponent<TotalManager_3>().MainGameOn();
		GameObject.Find("Manager").GetComponent<UIManager>().TextReset();
		gameObject.SetActive(false);
    }

    //adding bonus to normal score after fever ends
    void FeverBonus()
    {
        int bonusAfterFever = GameObject.Find("Main Camera").GetComponent<TotalManager_3>().bonusAfterFever;
        GameObject.Find("Manager").GetComponent<UIManager>().seconds += bonusAfterFever;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Falling" && fevercall == false)
        {
            fevercall = true;
            Debug.Log("stayfor3sec");
            StartCoroutine("TimeExpand");            
        }
    }   
}
