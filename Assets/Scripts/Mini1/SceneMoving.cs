using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoving : MonoBehaviour {
    //GameObject DataManager;

    // Use this for initialization
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        //DataManager = GameObject.Find("DataManager");
    }

    public void PlayAgain(string SceneName)
    {
        //DataManager.GetComponent<ControlGameData>().Save("money");
        //DataManager.GetComponent<ControlGameData>().Save("puzzle");
        SceneManager.LoadScene(SceneName);
    }

    public void BacktoHome()
    {
       //DataManager.GetComponent<ControlGameData>().Save("money");
       //DataManager.GetComponent<ControlGameData>().Save("puzzle");
        SceneManager.LoadScene("Main");
    }
}
