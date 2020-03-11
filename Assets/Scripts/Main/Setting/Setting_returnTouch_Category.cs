using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//효과음인지 뭔지
public class Setting_returnTouch_Category : MonoBehaviour {
    GameObject SettingManager;
    string objName;
	// Use this for initialization
	void Start () {
        //gameObject의 이름은 Settign_V_Up / Down
        SettingManager = GameObject.Find("Real_Setting");
    }

    // Update is called once per frame
    void OnMouseDown () {
        objName = gameObject.name;
        objName = objName.Substring(8);
        Debug.Log(objName);
        SettingManager.GetComponent<SettingScript>().changeCategory(objName);
	}
}
