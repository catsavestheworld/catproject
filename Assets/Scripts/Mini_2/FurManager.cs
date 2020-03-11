using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트 풀링을 사용해 털들이 들어오고 + 나가는 코드를 수행
public class FurManager : MonoBehaviour
{

	Sprite furspr;

    int fursize = 14;
    public float waitTime = 1;


    public Queue<GameObject> fur_q = new Queue<GameObject>();

    GameObject GameManager;

    // Use this for initialization
    void Start()
    {
	furspr = Resources.Load<Sprite> ("Pic_3/fur");
        GameObject inst_fur = gameObject.transform.Find("Cat_furs").gameObject;
        GameManager = GameObject.Find("GameManager");

        fur_q.Enqueue(inst_fur);
        inst_fur.SetActive(false);

        for (int i = 0; i < fursize; i++)
        {
            GameObject obj_fur = (GameObject)Instantiate(inst_fur);
            setPos(obj_fur);
            obj_fur.transform.parent = gameObject.transform;
            fur_q.Enqueue(obj_fur);
            obj_fur.SetActive(false);
        }

        StartCoroutine(appearFur());
    }

    public IEnumerator appearFur()
    {
        yield return new WaitForSeconds(waitTime);
        if (fur_q.Count != 0)
        {
			GameObject fur = fur_q.Dequeue ();
            fur.SetActive(true);
			fur.GetComponent<SpriteRenderer> ().sprite = furspr;
            fur.GetComponent<GetClick_fur>().settingPos();
            GameManager.GetComponent<TotalManager>().TotalFurNum++;
            GameManager.GetComponent<TotalManager>().appearFurText();

            if (GameManager.GetComponent<TotalManager>().TotalFurNum >= 12 && GameManager.GetComponent<TotalManager>().startcor == false)
            {
                GameManager.GetComponent<TotalManager>().startcor = true;
            }

        }
        StartCoroutine("appearFur");

    }

    //랜덤으로 포지션을 정해주는 함수
    void setPos(GameObject obj)
    {
        float xpos = Random.Range(-7, 7);
        float ypos = Random.Range(-4, 4);
        obj.transform.position = new Vector3(xpos, ypos, 0);
    }

    //클릭이 감지된 함수의 위치를 재설정해주고 큐에 넣은 후 disable시켜줌.
    public void GetObj(GameObject obj)
    {
        setPos(obj);//위치 재설정해주고
        fur_q.Enqueue(obj);//큐에 넣어주고
        obj.SetActive(false);//disable
        GameManager.GetComponent<TotalManager>().TotalFurNum--;
        GameManager.GetComponent<TotalManager>().appearFurText();
    }

}
