using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalManager : MonoBehaviour
{
    public int TotalFurNum;

    float gameOverTime;
    float setgameoverTimer;

    Scrollbar DustBar;
    GameObject Handle;
    Text FurText;
    Text GameOverText;

    public bool startcor;

    GameObject MiniGame3_Manager;

    Color green;
    Color yellow;
    Color orange;
    Color red;

    public  int gameBonus;

    // Use this for initialization
    void Start()
    {
        TotalFurNum = 0;

        setgameoverTimer = 5;
        gameOverTime = setgameoverTimer;
        startcor = false;

        FurText = GameObject.Find("FurText").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOverTimer").GetComponent<Text>();

        DustBar = GameObject.Find("DustBar").GetComponent<Scrollbar>();
        Handle = DustBar.transform.Find("SlidingArea").gameObject.transform.Find("Handle").gameObject;
        green = new Color(158f/255f, 229f/255f, 64f/255f,255/255);
        yellow = new Color(240f/255f, 228f/255f, 72f/255f, 255/255);
        orange = new Color(249f/255f, 146f/255f, 66f/255f, 255/255);
        red = new Color(236f/255f, 104f/255f, 104f/255f, 255/255);

        MiniGame3_Manager = GameObject.Find("MainCamera");

        FurText.text = "";
        GameOverText.text = "";

        appearFurText();
        StartCoroutine("countSeconds");

        gameBonus = 0;
           
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startcor == true)
        {
            if (TotalFurNum >= 12)
            {
                gameOverTime -= Time.deltaTime;
                GameOverText.text = System.Math.Round(gameOverTime, 2).ToString();

                if (gameOverTime < 0)
                {
                    MiniGame3_Manager.GetComponent<Minigame3_Mananger>().callGameover();
                    startcor = false;
                }
            }

            if (TotalFurNum < 12)
            {
                GameOverText.text = "";
                setgameoverTimer -= 0.5f;
                gameOverTime = setgameoverTimer;
                startcor = false;
            }
        }
    }

    public void appearFurText()
    {
        FurText.text = TotalFurNum.ToString();
        switch (TotalFurNum/3)
        {
            case 0:
                Handle.GetComponent<Image>().color = green;
                //Handle.GetComponent<Image>().color = Color.green ;
                break;
            case 1:
            case 2:
                Handle.GetComponent<Image>().color = yellow;
                //Handle.GetComponent<Image>().color = Color.yellow;
                break;
            case 3:
                Handle.GetComponent<Image>().color = orange;
                //Handle.GetComponent<Image>().color = Color.blue;
                break;
            default:
                Handle.GetComponent<Image>().color = red;
                //Handle.GetComponent<Image>().color = Color.red;
                break;

        }
        //Debug.Log(Handle.GetComponent<Image>().color);
        float barsize;
        if (TotalFurNum >= 12)
            barsize = 1;
        else
        {
            barsize = (float)TotalFurNum / 12f;
        }
        DustBar.GetComponent<Scrollbar>().size = barsize;
    }

    public IEnumerator GameBonus()
    {
        Debug.Log("hI!!!!");
        yield return new WaitForSeconds(15f);
        Debug.Log("wAIT IS OVER");
        MiniGame3_Manager.GetComponent<Minigame3_Mananger>().normalscore += 30;
        MiniGame3_Manager.GetComponent<Minigame3_Mananger>().showNormalscore();

        StartCoroutine("GameBonus");
    }

    public IEnumerator countSeconds()
    {
        yield return new WaitForSeconds(0.1f);
        MiniGame3_Manager.GetComponent<Minigame3_Mananger>().normalscore++;
        MiniGame3_Manager.GetComponent<Minigame3_Mananger>().showNormalscore();

        StartCoroutine("countSeconds");
    }

}
