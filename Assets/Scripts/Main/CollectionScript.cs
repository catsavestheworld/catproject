using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : CommonJob
{
    GameObject IntroObj;

    GameObject AudioManager;
    AudioClip cannotshow;
    Vector3 volVector;
    float effectvolume;

    //고양이, 가구 배치를 위해서 메인화면에서의 오브젝트들 읽어와야함(setactive true, false 위해서)

    GameObject BaseObj;
    GameObject ExitButton;
    GameObject[] Category = new GameObject[4];

    GameObject GoodsParent;
    GameObject[] Goods = new GameObject[10];
    GameObject StoryParent;
    GameObject[] Story = new GameObject[3];
    GameObject PuzzleParent;
    GameObject PuzzleBaseObj;
    GameObject[] Puzzle = new GameObject[8];
    GameObject[] PuzzleArrow = new GameObject[2];

    GameObject InfoObj;
    //GameObject InfoButton_Exit;
    GameObject InfoButton_Placement;

    Sprite[] BaseSpr = new Sprite[4];
    Sprite[] catSpr = new Sprite[8];
    Sprite[] catInfoSpr = new Sprite[8];
    Sprite[] furnitureSpr = new Sprite[9];
    Sprite[] furnitureInfoSpr = new Sprite[9];
    Sprite[] placementSpr = new Sprite[2];
    Sprite[] puzzlebaseSpr = new Sprite[6];
    Sprite[][] puzzlepieceSpr = new Sprite[6][];

    Sprite nothinggoodsSpr;
    Sprite willbeUpdated;

    /* 콜렉션에 필요한, 데이터에서 읽어와야 하는 정보들
      고양이의 구매 여부
      가구의 구매 여부 및 디벨롭 수준, 설치 정보
      소유한 금액
     */
    int[] buycat = new int[8]; //정수를 읽어와서 이진수로 나누면서 판단해야 함 --> 128 : 8번째만 구매함

    int[] furniture = new int[9]; //구매여부 및 디벨롭 여부 판가름할것. -1(구매안함)/012(구매&레벨업에 따라 1/2/3), 설치한 것은 레벨따라 345
    int[] playnum = new int[3];
    int[] puzzle = new int[6];//각 미니게임 3개마다 퍼즐 2개, 각 퍼즐은 총 8개 조각 --> 이는 이진수로 저장되어있음.

    int[][] realpuzzle = new int[6][];//이진수로 변환된 퍼즐 개수를 받아오는! 변수

    bool[] activecategory = new bool[4]; // cat-furniture-story

    string sprdir;
    int i;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        AudioManager = GameObject.Find("AudioManager");


        sprdir = "Main/ShopAndCollection/";

        IntroObj = GameObject.Find("Intro_Story");

        BaseObj = GameObject.Find("Base_Collection");
        ExitButton = GameObject.Find("C_ExitButton");
        Category[0] = GameObject.Find("CollectionCategory_C");
        Category[1] = GameObject.Find("CollectionCategory_F");
        Category[2] = GameObject.Find("CollectionCategory_S");
        Category[3] = GameObject.Find("CollectionCategory_P");

        GoodsParent = GameObject.Find("C_Goods");

        for(i=0;i<Goods.Length;i++){
            Goods[i] = GameObject.Find("C_Goods_" + i);
        }
        for (i = 0; i < 4; i++)
        {
            BaseSpr[i] = Resources.Load<Sprite>("Main/CollectionSprite/" + "base_" + i);

            catSpr[i] = Resources.Load<Sprite>(sprdir + "Collection_Cat_" + i);
            catSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Collection_Cat_" + (i + 4));
            furnitureSpr[i] = Resources.Load<Sprite>(sprdir + "Collection_F_" + i);
            furnitureSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Collection_F_" + (i + 4));
            catInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_C_" + i);
            catInfoSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Info_C_" + (i + 4));
            furnitureInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_F_" + i);
            furnitureInfoSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Info_F_" + (i + 4));
        }
        furnitureSpr[8] = Resources.Load<Sprite>(sprdir + "Collection_F_8");
        furnitureInfoSpr[8] = Resources.Load<Sprite>(sprdir + "Info_F_8");
        willbeUpdated = Resources.Load<Sprite>(sprdir+"willbeUpdated");

        StoryParent = GameObject.Find("C_Story");
        for (i = 0; i < Story.Length; i++)
        {
            Story[i] = GameObject.Find("C_Story_" + i);
        }

        PuzzleParent = GameObject.Find("C_Puzzle");
        PuzzleBaseObj = GameObject.Find("C_Puzzle_Base");
        PuzzleArrow[0] = GameObject.Find("C_Puzzle_Left");
        PuzzleArrow[1] = GameObject.Find("C_Puzzle_Right");

        for (i = 0; i < 6; i++)
        {
            realpuzzle[i] = new int[8];
            puzzlepieceSpr[i] = new Sprite[8];
            puzzlebaseSpr[i] = Resources.Load<Sprite>("Main/CollectionSprite/Puzzle/" + "Puzzle_Base_" + i);
            for (int j = 0; j < 8; j++)
            {
                puzzlepieceSpr[i][j] = Resources.Load<Sprite>("Main/CollectionSprite/Puzzle/Puzzle_piece_" + i + "/Puzzle_" + j);
            }
        }

        for (i = 0; i < 8; i++)
            Puzzle[i] = GameObject.Find("C_P_" + i);


        InfoObj = GameObject.Find("Collection_Info");
        //InfoButton_Exit = GameObject.Find("Exit_Button");
        InfoButton_Placement = GameObject.Find("Placement_Button");

        placementSpr[0] = Resources.Load<Sprite>("Main/CollectionSprite/Collection_info_placement");
        placementSpr[1] = Resources.Load<Sprite>("Main/CollectionSprite/Collection_info_disposition");

        nothinggoodsSpr = Resources.Load<Sprite>("Main/CollectionSprite/Collection_Goods_Nothing");

        for (i = 0; i < activecategory.Length; i++)
            activecategory[i] = false;

        InfoObj.SetActive(false);
        StoryParent.SetActive(false);
        PuzzleParent.SetActive(false);
        GoodsParent.SetActive(false);
        BaseObj.SetActive(false);
    }

    public override void initial()
    {
        //데이터를 읽어오고 초기화시키기
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();
        playnum = DataManager.GetComponent<ControlGameData>().getPlaynum();
        buycat = DataManager.GetComponent<ControlGameData>().getBuycat();
        puzzle = DataManager.GetComponent<ControlGameData>().getPuzzle();

        cannotshow = AudioManager.GetComponent<Main_AudioManager>().Shop_cannotbuy;
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        //총 6개의 퍼즐에 대해 해당 퍼즐의 조각들을 
        for (i = 0; i < 6; i++)
        {
            int tempPuzzle = puzzle[i];
            for (int j = 0; j < 8; j++)
            {
                realpuzzle[i][j] = tempPuzzle % 2;
                tempPuzzle /= 2;
            }
        }


        //기본적으로 고양이를 켜고 시작하는 것으로.
        SettingCategory(0);
        BaseObj.SetActive(true);
    }

    public override void finish()
    {
        base.finish();
        //해당 작업을 끝내기.
        save();
        InfoObj.SetActive(false);
        StoryParent.SetActive(false);
        PuzzleParent.SetActive(false);
        GoodsParent.SetActive(false);
        BaseObj.SetActive(false);
        PlacementObj.GetComponent<PlacementScript>().Placement();
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }

    public override void save()
    {
        DataManager.GetComponent<ControlGameData>().setFurniture(furniture);
        DataManager.GetComponent<ControlGameData>().setBuycat(buycat);
        DataManager.GetComponent<ControlGameData>().Save("furniture");
        DataManager.GetComponent<ControlGameData>().Save("cat");
    }

    //카테고리 버튼 누를 때 생기는 변화 --> cat, 가구의 경우에는 sprite 세팅도 함께.
    public void SettingCategory(int index)
    {
        for (i = 0; i < 4; i++)
        {
            if (i == index)
            {
                BaseObj.GetComponent<SpriteRenderer>().sprite = BaseSpr[i];
                activecategory[i] = true;
            }

            else
                activecategory[i] = false;
        }

        if (index < 2)
        {
            GoodsParent.SetActive(true);
            StoryParent.SetActive(false);
            PuzzleParent.SetActive(false);
            showGoods(index);
        }
        else if (index == 2)
        {
            GoodsParent.SetActive(false);
            PuzzleParent.SetActive(false);
            StoryParent.SetActive(true);
        }
        else if (index == 3)
        {
            GoodsParent.SetActive(false);
            StoryParent.SetActive(false);
            PuzzleParent.SetActive(true);
            showPuzzle(0); // 0번째 퍼즐부터 보여주기 시작한다.
        }
    }

    void showGoods(int index)
    {
        switch (index)
        {
            case 0:
                for (i = 0; i < Goods.Length; i++)
                {
                    if(i >= catSpr.Length){
                        Goods[i].GetComponent<SpriteRenderer>().sprite = willbeUpdated;
                        continue;
                    }
                    if (buycat[i] != -1)
                    {
                        Goods[i].GetComponent<SpriteRenderer>().sprite = catSpr[i];
                    }
                    else{

                    }

                }
                break;
            case 1:
                for (i = 0; i < Goods.Length; i++)
                {
                    if(i >= furniture.Length){
                        Goods[i].GetComponent<SpriteRenderer>().sprite = willbeUpdated;
                        continue;
                    }
                    if (furniture[i] != -1)
                    {
                        Goods[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i];
                    }
                    else{
                        Goods[i].GetComponent<SpriteRenderer>().sprite = nothinggoodsSpr;
                    }
                }
                break;
            default:
                break;
        }
    }

    void showPuzzle(int index)
    {
        PuzzleBaseObj.GetComponent<SpriteRenderer>().sprite = puzzlebaseSpr[index];
        for (i = 0; i < 8; i++)
        {
            if (realpuzzle[index][i] == 0)
            {
                Puzzle[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                Puzzle[i].GetComponent<SpriteRenderer>().sprite = puzzlepieceSpr[index][i];
            }
        }
    }

    public void puzzleArrow(string dir)
    {
        string sprname = PuzzleBaseObj.GetComponent<SpriteRenderer>().sprite.name;
        int nowIndex = int.Parse(sprname.Substring(sprname.Length - 1));
        if (dir == "Left")
        {
            if (nowIndex != 0)
                showPuzzle(--nowIndex);
        }
        else if (dir == "Right")
        {
            if (nowIndex != 5)
                showPuzzle(++nowIndex);
        }
    }

    public void showInfo(int num)//해당 오브젝트에서 자신의 spritenum 잘라서 보내기{
    {
        if (activecategory[0])
        {
            if (num >= catSpr.Length){
                return;
            }
        }
        else if(activecategory[1]){
            if(num >= furniture.Length){
                return;
            }
        }
        turnOffCollider("Goods");
        InfoObj.SetActive(true);
        if (activecategory[0] == true)
        {
            //고양이
            InfoObj.GetComponent<SpriteRenderer>().sprite = catInfoSpr[num];
            InfoButton_Placement.GetComponent<SpriteRenderer>().sprite = placementSpr[buycat[num]];
        }
        else if (activecategory[1] == true)
        {
            //가구
            InfoObj.GetComponent<SpriteRenderer>().sprite = furnitureInfoSpr[num];
            InfoButton_Placement.GetComponent<SpriteRenderer>().sprite = placementSpr[furniture[num]];
        }
    }

    public void offInfo()
    {
        InfoObj.SetActive(false);
        turnOnCollider("Goods");
    }

    /*이 함수는 나중에 오브젝트들 다 나오면 설정하기.
  해당 오브젝트의 index num을 전달해주니까 가구인지/고양이인지 체크해서 배치*/
    public void placeObj(int index)
    {
        if (activecategory[0] == true)
        {
            //고양이
            buycat[index] = (buycat[index] + 1) % 2;
            InfoButton_Placement.GetComponent<SpriteRenderer>().sprite = placementSpr[buycat[index]];
        }
        else if (activecategory[1] == true)
        {
            //가구
            furniture[index] = (furniture[index] + 1) % 2;
            InfoButton_Placement.GetComponent<SpriteRenderer>().sprite = placementSpr[furniture[index]];
        }
    }

    public void showStory(int num)//해당 오브젝트에서 자신의 스테이지num 잘라서 보내기
    {
        turnOffCollider("Story");
        if (playnum[num] != 0)
            IntroObj.GetComponent<ShowingIntro>().callingIntro(gameObject, num);
        else
        {
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(cannotshow, volVector);

            turnOnCollider("Story");
        }

    }

    public void finishStory()
    {
        turnOnCollider("Story");
    }

    void turnOffCollider(string category)
    {
        ExitButton.GetComponent<BoxCollider2D>().enabled = false;
        for (i = 0; i < Category.Length; i++)
        {
            Category[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        switch (category)
        {
            case "Goods":
                for (i = 0; i < Goods.Length; i++)
                {
                    Goods[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
            case "Story":
                for (i = 0; i < Story.Length; i++)
                {
                    Story[i].GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
        }
    }

    //꺼줬던 콜라이더 켜 주는 작업 수행
    void turnOnCollider(string category)
    {
        ExitButton.GetComponent<BoxCollider2D>().enabled = true;
        for (i = 0; i < Category.Length; i++)
        {
            Category[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        switch (category)
        {
            case "Goods":
                for (i = 0; i < Goods.Length; i++)
                {
                    Goods[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                break;
            case "Story":
                for (i = 0; i < Story.Length; i++)
                {
                    Story[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                break;
        }
    }
}
