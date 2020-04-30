using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    GameObject DataManager;
    int[] buycat;
    public List<int> boughtCat = new List<int>();
    int tempcatindex;

    int catsize = 8;

    float assignedTime = 30;
    public float waitTime = 15;
    public float realWaitTime;

    public bool nowWait;

    public int nowCat;
    public List<GameObject> CatList = new List<GameObject>();

    List<Vector3> CatPos = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        DataManager = GameObject.Find("DataManager");

        boughtCat = new List<int>();

        buycat = DataManager.GetComponent<ControlGameData>().getBuycat();
        tempcatindex = 0;
        boughtCat.Add(0);

        for (int i = 1; i < buycat.Length; i++)
        {
            if (buycat[i] != -1)
                boughtCat.Add(i);
        }

        assignedTime = 30;
        waitTime = 10;

        nowCat = 0;

        GameObject inst_cat = GameObject.Find("Cat");

        CatPos.Add(new Vector3(11.75f, 1.1f, 0));
        CatPos.Add(new Vector3(2.8f, 0.7f, 0));
        CatPos.Add(new Vector3(-0.25f, -1.35f, 0));
        CatPos.Add(new Vector3(14.45f, -1.2f, 0));
        CatPos.Add(new Vector3(5.6f, 2.7f, 0));
        CatPos.Add(new Vector3(-0.35f, 2.1f, 0));
        CatPos.Add(new Vector3(8.5f, 0.6f, 0));
        CatPos.Add(new Vector3(13.4f, 4.1f, 0));

        for (int i = 0; i < catsize; i++)
        {
            GameObject obj_cat = (GameObject)Instantiate(inst_cat);
            int j = Random.Range(0, boughtCat.Count);
            tempcatindex = boughtCat[j];
            obj_cat.GetComponent<DemandManager>().settingSprite(tempcatindex);
            setPos(obj_cat);
            obj_cat.transform.parent = gameObject.transform;

            CatList.Add(obj_cat);
            obj_cat.SetActive(false);
        }

        inst_cat.SetActive(false);

        realWaitTime = waitTime;
        nowWait = false;

        GameObject cat = CatList[nowCat];
        cat.SetActive(true);
        nowCat++;

        StartCoroutine("appearCat");
    }

    private void Update()
    {
        if (nowWait == true)
            realWaitTime -= Time.deltaTime;
    }


    public IEnumerator appearCat()
    {
        if (nowCat == 8)
            StopCoroutine("appearCat");

        if (nowCat != 8)
        {
            nowWait = true;
            yield return new WaitForSeconds(waitTime);
            nowWait = false;
            GameObject cat = CatList[nowCat];
            cat.SetActive(true);
            nowCat++;
            assignedTime = 10 + (nowCat - 1) * 15;
            waitTime = assignedTime;
            realWaitTime = waitTime;
            StartCoroutine("appearCat");
        }

    }

    void setPos(GameObject obj)
    {
        int index = Random.Range(0, CatPos.Count);
        obj.transform.position = CatPos[index];

        CatPos.RemoveAt(index);
    }

}
