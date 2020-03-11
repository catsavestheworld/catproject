using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonJob : MonoBehaviour {

    //데이터와 메인매니저코드를 가지고 있는 오브젝트인 MainManager
    public GameObject DataManager;
    public GameObject MainManager;
    public GameObject PlacementObj;

    public virtual void Start()
    {
        DataManager = GameObject.Find("DataManager");
        MainManager = GameObject.Find("MainManager");
        PlacementObj = GameObject.Find("Placement");
    }

    public virtual void initial()
    {
        //데이터를 읽어오고 초기화시키기
        //Debug.Log("virtual function");
    }

    public virtual void finish()
    {
        //해당 작업을 끝내기.
        PlacementObj.GetComponent<PlacementScript>().OnCatCollider();
    }

    public virtual void save()
    {
        //데이터값 저장
    }
}
