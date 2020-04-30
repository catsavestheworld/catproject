using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void pauseTime()
    {
        Time.timeScale = 0;
    }
    public void resumeTime()
    {
        Time.timeScale = 1;
    }
}
