using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//changeStage : 미니게임 스테이지 변화
public class Setting_returnTouch_Index : MonoBehaviour {
    GameObject SettingManager;
    string objName;
    // Use this for initialization
    void Start()
    {
        //gameObject의 이름은 Settign_V_Up / Down
        SettingManager = GameObject.Find("Real_Setting");
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        objName = gameObject.name;
        objName = objName.Substring(10);
        Debug.Log(objName);
        SettingManager.GetComponent<SettingScript>().changeStage(objName);
    }
}
