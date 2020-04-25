using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    //used to create 8 boxes
    GameObject box, tube;
    public Queue<GameObject> inItem = new Queue<GameObject>();
    public Queue<GameObject> outItem = new Queue<GameObject>();
    int boxCount;

    Vector3 spawnPlace = new Vector3(-11.5f, -0.35f, 1); //where the object is spawned (on top of the conveyor belt)

    GameObject feverTimeFrame;
    GameObject dust;


    Sprite boxSpr;
    
    void Start ()
    {
        tube = GameObject.Find("Tube");
        box = GameObject.Find("Box");
        boxSpr = Resources.Load<Sprite>("Sprites/Item/Object_box");

        feverTimeFrame = GameObject.Find("FeverTimeFrame");
        dust = GameObject.Find("DustUp");

        inItem.Enqueue(box);

        for (int i = 0; i < 8; i++)
        {
            GameObject obj_box = (GameObject)Instantiate(box);
            obj_box.transform.parent = gameObject.transform;
            inItem.Enqueue(obj_box);
            obj_box.SetActive(false);
        }

        box.SetActive(false);

        CallBox();       
    }

    //spawning box on top of the conveyor belt
    public void CallBox()
    {       
        GameObject box_out = inItem.Dequeue(); //getting box out of queue
        outItem.Enqueue(box_out);
        box_out.GetComponent<SpriteRenderer>().sprite = boxSpr;  //chaning sprite to box (since box come very first)
        box_out.transform.position = spawnPlace; //position = on top of conveyor belt
        box_out.SetActive(true);
    }

    //putting object back into queue
    public void OrganizeBox(GameObject obj)
    {
        obj = outItem.Dequeue();
        inItem.Enqueue(obj);//큐에 넣어주고
    }

    IEnumerator OrganizeTube()
    {
        float tube_x = tube.transform.position.x;
        while (tube_x > -14.8f)
        {
            tube.GetComponent<Transform>().position -= new Vector3(0.065f, 0, 0);
            tube_x = tube.transform.position.x;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator CheckBoxNum()
    {
        GameObject box_organize = outItem.Dequeue();
        box_organize.GetComponent<SpriteRenderer>().sortingOrder = 0;
        inItem.Enqueue(box_organize);
        box_organize.GetComponent<MovingBox>().SetBool();
        box_organize.SetActive(false);
        boxCount = inItem.Count;
        

        if (boxCount == 9)
        {
            // get ready for Fever Time
            feverTimeFrame.SetActive(true);

            yield return new WaitForSeconds(0.3f);

            StartCoroutine("OrganizeTube");
            Debug.Log("Done");
            yield return new WaitForSeconds(1.9f);
            
            feverTimeFrame.SetActive(false);            
            GameObject.Find("Main Camera").GetComponent<TotalManager_3>().IsFever();

            StopCoroutine("CheckBoxNum");
        }

        yield return 0;
        StartCoroutine("CheckBoxNum");
    }    

    //to FeverTime
    public void StopBox()
    {
        StartCoroutine("CheckBoxNum");
    }
}
