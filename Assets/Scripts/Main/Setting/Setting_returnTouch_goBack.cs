using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_returnTouch_goBack : MonoBehaviour {
    GameObject SettingManager;
    
    void Start()
    {
        //gameObject의 이름은 Settign_V_Up / Down
        SettingManager = GameObject.Find("Real_Setting");
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        SettingManager.GetComponent<SettingScript>().finish();
    }
}
