using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    GameObject DataManager;

    GameObject[] furnitureObj = new GameObject[9];
    public GameObject[] catObj = new GameObject[8];

    int[] furniture = new int[9];
    int[] buycat = new int[8];

    int[] furniture_alloc = new int[9];
    int[] nowIndex = new int[9];//지나간 인덱스 세기.

    int allocation; //총 배치해야 하는 위치 수

    int i;

    //0~14는 가구에 배치되는 위치, 15~19는 free한 위치에 있는 경우  
    Sprite[][] catSprite = new Sprite[8][];

    //가구들의 인덱스에 따른 위치, 총 15개
    List<Vector3> furniturePos;

    //free한 위치에 있는 경우에 대비한 벡터 리스트.
    //unlock/lock 여부에 따라 pos값 달라진다.
    List<Vector3> freePos_lock;
    List<Vector3> freePos_unlock;

    List<Vector3> freePos;

    //고양이가 놓일 위치. allocation과 size가 같아야 함.
    List<Vector3> catPlacement;


    // Use this for initialization
    void Start()
    {
        DataManager = GameObject.Find("DataManager");



        for (i = 0; i < 9; i++) {
            furnitureObj[i] = GameObject.Find("Furniture_" + i);
        }
        
        for (i = 0; i<8; i++) {
            catObj[i] = GameObject.Find("Cat_" + i);
            catSprite[i] = new Sprite[21];
            for (int j = 0; j < 21; j++)
            {
                catSprite[i][j] = Resources.Load<Sprite>("Main/CatSprite/Cat_" + i + "/Cat_" + i + "_" + j);
            }
        }

        SettingPos();

        furniture_alloc[0] = 1;
        furniture_alloc[1] = 2;
        furniture_alloc[2] = 1;
        furniture_alloc[3] = 1;
        furniture_alloc[4] = 1;
        furniture_alloc[5] = 2;
        furniture_alloc[6] = 4;
        furniture_alloc[7] = 2;
        furniture_alloc[8] = 1;//캣휠

        DataManager.GetComponent<ControlGameData>().Load();
        OnCatCollider();
        Placement();
    }

    void SettingPos()
    {
        furniturePos = new List<Vector3>();
        freePos_lock = new List<Vector3>();
        freePos_unlock = new List<Vector3>();
        catPlacement = new List<Vector3>();

        //furniturePos,freePos_lock과 freePos_unlock은 여기서 세팅
        furniturePos.Add(new Vector3(2, -0.6f, 0)); //0
        furniturePos.Add(new Vector3(6.09f, -1.63f, 0));//1
        furniturePos.Add(new Vector3(9.1f, -4.7f, 0));//2
        furniturePos.Add(new Vector3(-1.47f, -0.68f, 0));//3
        furniturePos.Add(new Vector3(8.3f, -0.6f, 0));//4
        furniturePos.Add(new Vector3(25.43f, 1.8f, 0));//5
        furniturePos.Add(new Vector3(29.57f, -1.38f, 0));//6
        furniturePos.Add(new Vector3(26.8f, -2.35f, 0));//7
        furniturePos.Add(new Vector3(15f, 2.3f, 0));//8
        furniturePos.Add(new Vector3(17.9f, 4.7f, 0));//9
        furniturePos.Add(new Vector3(18.35f, 2.4f, 0));//10
        furniturePos.Add(new Vector3(15.45f, -0.1f, 0));//11
        furniturePos.Add(new Vector3(20f, -1.75f, 0));//12
        furniturePos.Add(new Vector3(23f, -1.4f, 0));//13
        furniturePos.Add(new Vector3(-7.2f, 1.6f, 0));//14

        freePos_unlock.Add(new Vector3(5.55f, -0.35f, 0));
        freePos_unlock.Add(new Vector3(-7.2f, -2.05f, 0));
        freePos_unlock.Add(new Vector3(1.8f, -4f, 0));
        freePos_unlock.Add(new Vector3(-0.8f, 3.3f, 0));
        freePos_unlock.Add(new Vector3(11.3f, 1.1f, 0));
        freePos_unlock.Add(new Vector3(22.1f, 2.4f, 0));
        freePos_unlock.Add(new Vector3(29.65f, -4.3f, 0));
        freePos_unlock.Add(new Vector3(29.65f, 2.9f, 0));

        freePos_lock.Add(new Vector3(5.55f, -0.35f, 0));
        freePos_lock.Add(new Vector3(-2.75f, 3.6f, 0));
        freePos_lock.Add(new Vector3(1.8f, -4f, 0));
        freePos_lock.Add(new Vector3(4.85f, -4f, 0));
        freePos_lock.Add(new Vector3(-0.1f, 3.6f, 0));
        freePos_lock.Add(new Vector3(4.8f, 3.5f, 0));
        freePos_lock.Add(new Vector3(-4.7f, -1.9f, 0));
        freePos_lock.Add(new Vector3(-8.7f, -2.1f, 0));
    }

    void getData()
    {
        furniture = DataManager.GetComponent<ControlGameData>().getFurniture();
        buycat = DataManager.GetComponent<ControlGameData>().getBuycat();

    }

    public void Placement()
    {
        SettingPos();
        getData(); //데이터를 다시 읽어옴
        judgeLocked();
        allocation = 0;

        //unlock인지 lock인지 판단해서 위치 벡터 가져오기
        bool unlocked = judgeLocked();

        if (unlocked == true)//언록되어있다면
            freePos = freePos_unlock;
        else if (unlocked == false)
            freePos = freePos_lock;

        nowIndex[0] = 0;



        //가구 배치 설정+고양이 설치 가능 공간 세기
        //설치 가능한 고양이 몇 마리인지 설정+설치 가능한 공간 몇 군데인지 마저 설정
        for (i = 0; i < 9; i++)
        {
            // Debug.Log(i);
            if (i == 0)
                nowIndex[i] = furniture_alloc[i];
            else
                nowIndex[i] = nowIndex[i - 1] + furniture_alloc[i];

            if (furniture[i] == 1)
            {
                allocation += furniture_alloc[i];
                furnitureObj[i].SetActive(true);

                if (i != 0)
                {
                    for (int j = nowIndex[i - 1]; j < nowIndex[i]; j++)
                    {
                        catPlacement.Add(furniturePos[j]);
                    }

                }
                else if (i == 0)
                {
                    catPlacement.Add(furniturePos[0]);
                }
            }
            else if (furniture[i] != 1)
                furnitureObj[i].SetActive(false);
        }

        for (i = 0; i <8; i++)
        {
            if (buycat[i] == 1)
            {
                catObj[i].SetActive(true);
                allocation++;
                int putindex = Random.Range(0, freePos.Count);
                catPlacement.Add(freePos[putindex]);
                freePos.RemoveAt(putindex);
            }

            else if (buycat[i] != 1)
                catObj[i].SetActive(false);

            //Debug.Log(allocation);
        }

        //고양이 배치 설정
        for (i = 0; i < 8; i++)
        {
            //Debug.Log(buycat[i]);
            //고양이가 배치 설정 된 고양이일 경우에만
            if (buycat[i] == 1)
            {
                int posIndex = Random.Range(0, catPlacement.Count);
                catObj[i].transform.localPosition = catPlacement[posIndex];

                int spriteIndex = furniturePos.IndexOf(catPlacement[posIndex]);

                if (spriteIndex == -1) {
                    //15번~20번 파일을 랜덤으로 출력
                    int randPos = Random.Range(15, 21); 
                    catObj[i].GetComponent<SpriteRenderer>().sprite = catSprite[i][randPos]; 
                }
                else
                    catObj[i].GetComponent<SpriteRenderer>().sprite = catSprite[i][spriteIndex];

                catPlacement.RemoveAt(posIndex);
            }
        }
    }
    

    bool judgeLocked()
    {
        int[] judgefurniture = new int[8];

        judgefurniture = DataManager.GetComponent<ControlGameData>().getFurniture();

        bool unlockcondition = true; // 일단 언록되어있다고 가정되고
        for (int j = 0; j < 4; j++)
        {
            if (judgefurniture[j] == -1)
                unlockcondition = false;//하나라도 앞 네개 중 구매 안 한 것 있으면 록시키기

        }

        return unlockcondition;
    }

    public void OffCatCollider()
    {
        for (int i = 0; i < catObj.Length; i++)
        {
            catObj[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void OnCatCollider()
    {
        for (int i = 0; i < catObj.Length; i++)
        {
            catObj[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
