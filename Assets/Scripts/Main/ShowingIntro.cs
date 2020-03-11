using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//초반 인트로를 보여줄 수 있게!
public class ShowingIntro : MonoBehaviour
{
    Text StoryText;

    public Sprite[][] introSprite = new Sprite[3][];
    string spritedir;

    GameObject Background;
    GameObject Collection_Skip;
    GameObject MiniGame_Loading;

    public bool finishIntro;
    public bool skipButton;

    // Use this for initialization
    void Start()
    {
        int i, j;

        StoryText = GameObject.Find("ImgText").GetComponent<Text>();

        Background = GameObject.Find("Story_Background");
        Collection_Skip = GameObject.Find("Collection_StorySkip");
        MiniGame_Loading = GameObject.Find("MiniGame_LoadingImg");

        introSprite[0] = new Sprite[7];
        introSprite[1] = new Sprite[7];
        introSprite[2] = new Sprite[6];

        for (i = 0; i < 3; i++)
        {
            for (j = 0; j < introSprite[i].Length; j++)
            {
                spritedir = "Story_" + (i + 1) + "/STORY_" + (j + 1);
                introSprite[i][j] = Resources.Load<Sprite>(spritedir);
            }
        }

        Background.SetActive(false);
        Collection_Skip.SetActive(false);
        MiniGame_Loading.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        finishIntro = false;

        StoryText.text = "";

    }

    public void skipStory()
    {
        skipButton = true;
    }

    public void callingIntro(GameObject callobj, int i)
    {
        StartCoroutine(callIntro(callobj, i));
    }

    public void showLoading(int num)
    {
        Background.SetActive(true);
        MiniGame_Loading.SetActive(true);
        gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = introSprite[num][introSprite[num].Length - 1];
    }

    //스테이지 선택할 때 얘로 코루틴 바로 돌려버리면 됨 
    IEnumerator callIntro(GameObject callobj, int stagenum)
    {
        skipButton = false;
        if (callobj.name == "Real_MiniGame")
            MiniGame_Loading.SetActive(true);
        if (callobj.name == "Real_Collection")
            Collection_Skip.SetActive(true);

        Background.SetActive(true);
        for (int i = 0; i < introSprite[stagenum].Length; i++)
        {
            if (skipButton == true && callobj.name == "Real_Collection")
                break;
            StoryText.text = (i + 1).ToString() + "/" + introSprite[stagenum].Length.ToString();
            gameObject.GetComponent<SpriteRenderer>().sprite = introSprite[stagenum][i];
            yield return new WaitForSeconds(1.5f);
        }
        StoryText.text = "";
        

        //미니게임에서 부른거면 게임 플레이로 돌아가고
        if (callobj.name == "Real_MiniGame")
        {
            callobj.GetComponent<MiniGameScript>().playGame(stagenum);
        }
            
        //콜렉션에서 부른거면 콜렉션 화면으로 다시 돌아감
        if (callobj.name == "Real_Collection")
        {
            Background.SetActive(false);
            Collection_Skip.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            callobj.GetComponent<CollectionScript>().finishStory();
        }

    }


}
