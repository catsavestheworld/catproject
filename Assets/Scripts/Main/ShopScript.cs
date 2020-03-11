using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopScript : CommonJob
{
    GameObject AudioManager;
    GameObject backButton;
    GameObject[] Button = new GameObject[2];
    GameObject[] Goods = new GameObject[4];
    GameObject[] seal_Buy = new GameObject[4];
    GameObject[] Arrow = new GameObject[2];
    GameObject Shop_Background;
    GameObject SelectObj;
    GameObject InfoObj;
    GameObject LockObj;

    AudioClip canBuy;
    AudioClip cannotBuy;
    Vector3 volVector;
    float effectvolume;

    Text MoneyText;

    Sprite sealSpr;
    Sprite lockedSpr;

    Sprite unlockwaySpr;

    Sprite[] catSpr = new Sprite[8];
    Sprite[] furnitureSpr = new Sprite[8];

    Sprite[] catInfoSpr = new Sprite[8];
    Sprite[] furnitureInfoSpr = new Sprite[8];

    string sprdir = "Main/ShopAndCollection/";


    int i;

    bool want_cat;
    bool want_furniture;

    /* 상점에 필요한, 데이터에서 읽어와야 하는 정보들
      고양이의 구매 여부
      가구의 구매 여부 및 디벨롭 수준
      소유한 금액
     */
    int[] buycat = new int[8]; // -1,0,1 세가지. 
    int[] furniture = new int[8]; //구매여부 및 디벨롭 여부 판가름할것. -1(구매안함)/012(구매&레벨업에 따라 1/2/3), 설치한 것은 레벨따라 345
    int money;

    /* 정의해줘야 하는 부분 
     고양이 가격
     가구별 가격 + 레별 별 가격변화
     */
    int[] catCost = new int[8];
    int[] furnitureCost = new int[8];


    // Use this for initialization
    public override void Start()
    {
        base.Start();

        catCost[0] = 2000;  furnitureCost[0] = 2000;
        catCost[1] = 8000;  furnitureCost[1] = 8000;
        catCost[2] = 10000;  furnitureCost[2] = 9000;
        catCost[3] = 12000;  furnitureCost[3] = 10000;
        catCost[4] = 14000;  furnitureCost[4] = 12000;
        catCost[5] = 16000;  furnitureCost[5] = 13000;
        catCost[6] = 18000;  furnitureCost[6] = 15000;
        catCost[7] = 20000;  furnitureCost[7] = 16000;

        AudioManager = GameObject.Find("AudioManager");
        canBuy = AudioManager.GetComponent<Main_AudioManager>().Shop_buy;
        cannotBuy = AudioManager.GetComponent<Main_AudioManager>().Shop_cannotbuy;
        

        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        MoneyText.text = "";

        backButton = GameObject.Find("Button_back");
        Button[0] = GameObject.Find("Button_cat");
        Button[1] = GameObject.Find("Button_furniture");

        Arrow[0] = GameObject.Find("Arrow_Left");
        Arrow[1] = GameObject.Find("Arrow_Right");

        InfoObj = GameObject.Find("Info_Obj");
        LockObj = GameObject.Find("LockedObj");

        //goods object를 찾아서 넣어주고
        //고양이 / 가구 sprite를 찾아서 넣어준다
        for (i = 0; i < 4; i++)
        {
            Goods[i] = GameObject.Find("Goods_" + i);
            seal_Buy[i] = Goods[i].transform.Find("SealSpr").gameObject;
            catSpr[i] = Resources.Load<Sprite>(sprdir + "Cat_" + i);
            catSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Cat_" + (i + 4));
            furnitureSpr[i] = Resources.Load<Sprite>(sprdir + "Furniture_" + i);
            furnitureSpr[i + 4] = Resources.Load<Sprite>(sprdir + "Furniture_" + (i + 4));
            catInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_C_" + i);
            catInfoSpr[i+4] = Resources.Load<Sprite>(sprdir + "Info_C_" + (i + 4));
            furnitureInfoSpr[i] = Resources.Load<Sprite>(sprdir + "Info_F_" + i);
            furnitureInfoSpr[i+4] = Resources.Load<Sprite>(sprdir + "Info_F_" + (i + 4));
        }

        sealSpr = Resources.Load<Sprite>("Main/ShopSprite/seal_bought");
        lockedSpr = Resources.Load<Sprite>("Main/ShopSprite/seal_locked");
        unlockwaySpr = Resources.Load<Sprite>("Main/ShopSprite/Message_unlockcondition");
        //Debug.Log(sealSpr.name);

        Shop_Background = GameObject.Find("Shop_Main"); // 
        SelectObj = GameObject.Find("Base_Shop");

        want_cat = false;
        want_furniture = false;

        
        backButton.SetActive(false);
        Button[0].SetActive(false);
        Button[1].SetActive(false);
        InfoObj.SetActive(false);
        LockObj.SetActive(false);
        SelectObj.SetActive(false);
        Shop_Background.SetActive(false);
        //Debug.Log("hi");
    }

    //데이터를 읽어오고 초기화시키기
    public override void initial()
    {
        
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        money = DataManager.GetComponent<ControlGameData>().getMoney();
        buycat= DataManager.GetComponent<ControlGameData>().getBuycat();
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();

        Shop_Background.SetActive(true);
        Button[0].SetActive(true);
        Button[1].SetActive(true);
        backButton.SetActive(true);

        MoneyText.text = money.ToString();
    }
    
    //해당 작업을 끝내기.
    public override void finish()
    {
        base.finish();

        Button[0].SetActive(false);
        Button[1].SetActive(false);
        backButton.SetActive(false);
        InfoObj.SetActive(false);
        LockObj.SetActive(false);
        SelectObj.SetActive(false);
        Shop_Background.SetActive(false);

        MoneyText.text = "";

        save();
        PlacementObj.GetComponent<PlacementScript>().Placement();
        MainManager.GetComponent<Main_Manager>().backtoMain();
    }

    public override void save()
    {
        DataManager.GetComponent<ControlGameData>().setMoney(money);
        DataManager.GetComponent<ControlGameData>().setFurniture(furniture);
        DataManager.GetComponent<ControlGameData>().setBuycat(buycat);
        DataManager.GetComponent<ControlGameData>().Save("cat");
        DataManager.GetComponent<ControlGameData>().Save("furniture");
        DataManager.GetComponent<ControlGameData>().Save("money");
    }

    //furniture / cat 중 뭘 눌렀는지 확인해서 로딩
    public void getCategorybutton(string objname)
    {
        SelectObj.SetActive(true);
        switch (objname)
        {
            case "Button_cat":
                want_cat = true;
                want_furniture = false;
                for (i = 0; i < 4; i++)
                {
                    Goods[i].GetComponent<SpriteRenderer>().sprite = catSpr[i];
                    if (buycat[i] != -1)
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = sealSpr;
                    else
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = null;
                }
                break;
            case "Button_furniture":
                want_cat = false;
                want_furniture = true;
                for (i = 0; i < 4; i++)
                {
                    Goods[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[i];
                    if (furniture[i] != -1)
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = sealSpr;
                    else
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = null;
                }
                break;
        }
    }

    //왼쪽인지 / 오른쪽인지 판단하고 고양이/가구 판단해서 sprite 로딩
    public void getArrow(string objname)
    {
        int index = 0;

        if (objname == "Arrow_Left")
            index = 0;
        else if(objname == "Arrow_Right")
            index = 4;

        if (want_cat == true)
        {
            for (i = 0; i < 4; i++, index++)
            {
                Goods[i].GetComponent<SpriteRenderer>().sprite = catSpr[index];
                if (buycat[index] != -1)
                    seal_Buy[i].GetComponent<SpriteRenderer>().sprite = sealSpr;
                else
                    seal_Buy[i].GetComponent<SpriteRenderer>().sprite = null;
                //Debug.Log(i + ", " + index);
            }
        }
        else if (want_furniture == true)
        {
            for (i = 0; i < 4; i++, index++)
            {
                Goods[i].GetComponent<SpriteRenderer>().sprite = furnitureSpr[index];
                if(index >= 4)//unlockcondition 체크해주기.
                {
                    if(judgeLocked() == false)//록되어있으면
                    {
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = lockedSpr;
                    }
                    else if(judgeLocked() == true) //언록되어있으면
                    {
                        MainManager.GetComponent<ChangeCameraPos>().turnOnObj();
                        if (furniture[index] != -1)
                            seal_Buy[i].GetComponent<SpriteRenderer>().sprite = sealSpr;
                        else
                            seal_Buy[i].GetComponent<SpriteRenderer>().sprite = null;
                    }
                }
                else
                {
                    if (furniture[index] != -1)
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = sealSpr;
                    else
                        seal_Buy[i].GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }
    }

    //가구 요구조건 다 만족시켰는지 확인하기
   bool judgeLocked()
    {
        bool unlockcondition = true; // 일단 언록되어있다고 가정되고
        for (int j=0; j < 4; j++)
        {
            if (furniture[j] == -1)
                unlockcondition = false;//하나라도 앞 네개 중 구매 안 한 것 있으면 언록시키기

        }

        return unlockcondition;
    }

    //해당 오브젝트의 설명을 띄워줌
    //해당 함수의 인자는 sprite의 이름을 보내준다.
    //이미 구매가 진행된 고양이/업그레이드가 완료된 가구의 경우에는 로딩 자체를 해주지 않는 것으로 한다.
    public void showObjinfo(string clickedSpr)
    {
        //string spritename = clickedObj.GetComponent<SpriteRenderer>().sprite.name;
        //int sprindex = int.Parse(spritename.Substring(spritename.Length - 1));
        int sprindex = int.Parse(clickedSpr.Substring(clickedSpr.Length - 1));

        if (want_cat == true && buycat[sprindex] == -1) //구매 안한 상품의 경우에만 설명을 보여주도록 한다.
        {
            InfoObj.SetActive(true);
            InfoObj.GetComponent<SpriteRenderer>().sprite = catInfoSpr[sprindex];
            turnOffCollider();
        }
        else if(want_furniture == true && furniture[sprindex] == -1)
        {
            
            if (sprindex >= 4 && judgeLocked() == false)
            {
                LockObj.SetActive(true);
                InfoObj.GetComponent<SpriteRenderer>().sprite = unlockwaySpr;
               
            }
            else
            {
                InfoObj.SetActive(true);
                InfoObj.GetComponent<SpriteRenderer>().sprite = furnitureInfoSpr[sprindex];
            }
            turnOffCollider();
        }
    }

    //해당 오브젝트의 설명을 끔 
    public void offObjinfo()
    {
        //해당 오브젝트 꺼주기
        InfoObj.SetActive(false);
        LockObj.SetActive(false);
        turnOnCollider();
    }

    //해당 클릭오브젝트는 (끄기 버튼) 해당 함수를 호출할때 자신의 parent의 sprite name을 매개변수로 넘긴다.
    public void buyObj(string clickedSpr)
    {
        //해당 오브젝트의 sprite 체크를 통해서 인덱스 받아옴
        //string spritename = clickedObj.GetComponent<SpriteRenderer>().sprite.name;
        //int sprindex = int.Parse(spritename.Substring(spritename.Length - 1));
        //Debug.Log(effectvolume);

        int sprindex = int.Parse(clickedSpr.Substring(clickedSpr.Length - 1));

        if (want_cat == true && buycat[sprindex] == -1)
        {
            if (money >= catCost[sprindex])
            {
                money -= catCost[sprindex];
                buycat[sprindex] = 1; // 기본적으로 설치시켜줌
                if (effectvolume != 0)
                    AudioSource.PlayClipAtPoint(canBuy, volVector);

                ShowMoney();
                ShowObjSeal(sprindex);

            }
            else
            {
                if (effectvolume != 0)
                    AudioSource.PlayClipAtPoint(cannotBuy, volVector);
            }
        }
        else if (want_furniture == true && furniture[sprindex] == -1)
        {
            if (money >= furnitureCost[sprindex])
            {
                //구매 진행 및 업그레이드 가격 갱신
                money -= furnitureCost[sprindex];
                furniture[sprindex] = 1; //기본적으로 설치한 후 시작
                if (effectvolume != 0)
                    AudioSource.PlayClipAtPoint(canBuy, volVector);
                ShowMoney();
                ShowObjSeal(sprindex);
            }
            else
            {
                if (effectvolume != 0)
                    AudioSource.PlayClipAtPoint(cannotBuy, volVector);
            }

            judgeLocked();

        }

        offObjinfo();
    }

    //몇번째 고양이를 산 건지 인덱스로 넘겨준다.
    void ShowObjSeal(int index)
    {
        Debug.Log(index);
        for (i = 0; i < 4; i++)
        {
            if(index%4 == i)   
                seal_Buy[i].GetComponent<SpriteRenderer>().sprite = sealSpr;
            //Debug.Log(i + ", " + index);
        }
    }

    void ShowMoney()
    {
        MoneyText.text = money.ToString();
    }

    //선택 과정에서 오류 없도록 콜라이더 꺼 주는 작업 수행
    void turnOffCollider()
    {
        backButton.GetComponent<BoxCollider2D>().enabled = false;
        for (i = 0; i < 2; i++)
        {
            Button[i].GetComponent<BoxCollider2D>().enabled = false;
            Arrow[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        for (i = 0; i < 4; i++)
        {
            Goods[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //꺼줬던 콜라이더 켜 주는 작업 수행
    void turnOnCollider()
    {
        backButton.GetComponent<BoxCollider2D>().enabled = true;
        for (i = 0; i < 2; i++)
        {
            Button[i].GetComponent<BoxCollider2D>().enabled = true;
            Arrow[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        for (i = 0; i < 4; i++)
        {
            Goods[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
