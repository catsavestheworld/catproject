using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeCoroutine : MonoBehaviour {

    GameObject GameManager;
    GameObject CatManager;

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager");
        CatManager = GameObject.Find("CatManager");
	}

    public void InvokingCoroutine()
    {
        StartCoroutine(GameManager.GetComponent<TotalManager>().countSeconds());
        StartCoroutine(CatManager.GetComponent<CatManager>().appearCat());
        if (GameManager.GetComponent<TotalManager>().gameBonus != 0)
            StartCoroutine(GameManager.GetComponent<TotalManager>().GameBonus());

        for (int i=0;i< CatManager.GetComponent<CatManager>().nowCat; i++)
        {
            GameObject tempcat = CatManager.GetComponent<CatManager>().CatList[i];
            
            if(tempcat.activeInHierarchy == true)
            {
                StartCoroutine(tempcat.GetComponent<FurManager>().appearFur());
                //고양이들이 모두 동시에 요구사항을 뱉지 않도록 하기 위해 waittime에 변경을 준다.
                tempcat.GetComponent<DemandManager>().waitTime = Random.Range(5, 7);
                StartCoroutine(tempcat.GetComponent<DemandManager>().appearDemand());
                StartCoroutine(tempcat.GetComponent<DemandManager>().shakingTail());
            }
        }
    }
}
