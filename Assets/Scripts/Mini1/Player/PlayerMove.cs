using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public int check = 0;

    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    

    void OnMouseDrag()
    {
        if (check == 0)
        {
            Vector2 mouseDragPosition = new Vector2(550, Input.mousePosition.y);
            Vector2 worldObjectPosition = Camera.main.ScreenToWorldPoint(mouseDragPosition);


            if (worldObjectPosition.y < 5.2f && worldObjectPosition.y > -5.4f)
            {
                Player.transform.position = worldObjectPosition;
                this.transform.position = worldObjectPosition;
            }
            
        }
        else { }
              
    }
}
