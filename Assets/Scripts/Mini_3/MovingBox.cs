using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    GameObject AudioManager;
    AudioClip PresentClicking;
    AudioClip PutBoxDown;
    AudioClip CatCrying;
    Vector3 volVector;
    float effectvolume;

    float speed;
    float timer;
    Sprite boxTo;

    //sprites
    Sprite boxSpr;
    Sprite catSpr;
    Sprite catFallSpr;
    Sprite[] toySpr = new Sprite[3];
    Sprite[] fakeSpr = new Sprite[3];
    Sprite[] fakeFallSpr = new Sprite[3];
    Sprite presentSpr;
    Sprite mySprite; // used to determine which sprite the object is

    //related to up&down drag
    bool ableDrag = false; //used to determine whether the object is in the range where items can be dragged
    Vector3 mousePosOn;
    GameObject dust;
    Sprite dustSpr;

    //for cat walking on the shelf
    int walkindex = 0;
    Sprite[] catWalking = new Sprite[2];

    //moving to position
    Vector3 startPos;
    Vector3 endPos;
    bool isUp;
    bool isDown;

    // gameObject.transform.Find("Cat").gameObject;

    void Start()
    {
        AudioManager = GameObject.Find("AudioManager");
        PresentClicking = AudioManager.GetComponent<Main_AudioManager>().PresentClicking;
        PutBoxDown = AudioManager.GetComponent<Main_AudioManager>().PutBoxDown;
        CatCrying = AudioManager.GetComponent<Main_AudioManager>().CatCrying;
        volVector = AudioManager.GetComponent<Main_AudioManager>().effectVector;
        effectvolume = AudioManager.GetComponent<Main_AudioManager>().effectVol;

        //finding sprite resources
        for (int i = 0; i < 2; i++)
        {
            toySpr[i] = Resources.Load<Sprite>("Sprites/Item/Object_toy_" + (i + 1));
            fakeSpr[i] = Resources.Load<Sprite>("Sprites/Item/Object_fake_" + (i + 1));
            fakeFallSpr[i] = Resources.Load<Sprite>("Sprites/Item/Object_fake_" + (i + 1) + "_drop");
            catWalking[i] = Resources.Load<Sprite>("Sprites/Cat_white_" + (i + 1));
        }
        toySpr[2] = Resources.Load<Sprite>("Sprites/Item/Object_toy_" + 3);
        fakeSpr[2] = Resources.Load<Sprite>("Sprites/Item/Object_fake_" + 3);
        fakeFallSpr[2] = Resources.Load<Sprite>("Sprites/Item/Object_fake_" + 3 + "_drop");
        catSpr = Resources.Load<Sprite>("Sprites/Item/Object_cat_white");
        catFallSpr = Resources.Load<Sprite>("Sprites/Item/Object_cat_white_drop");
        presentSpr = Resources.Load<Sprite>("Sprites/Item/Object_present");
        boxSpr = Resources.Load<Sprite>("Sprites/Item/Object_box");
        dustSpr = Resources.Load<Sprite>("Sprites/Dust");

        dust = GameObject.Find("DustUp");

        isUp = false;
        isDown = false;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        mySprite = this.GetComponent<SpriteRenderer>().sprite;

        timer = GameObject.Find("Manager").GetComponent<UIManager>().timer;
        speed = GameObject.Find("Manager").GetComponent<UIManager>().speed;

        float objPosition_x = gameObject.GetComponent<Transform>().position.x;

        startPos = this.transform.position;
        
        
        if (ableDrag == true)
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f; //distance of the plane from the camera
            var movePoint = Camera.main.ScreenToWorldPoint(screenPoint);

            // limit player from moving the box around the screen freely
            if (movePoint.x > 4.5f) 
            {
                movePoint.x = 4.5f;
            }
            else if(movePoint.x < -1.6f){
                movePoint.x = -1.6f;
            }
            if(movePoint.y < -2.9f)
            {
                movePoint.y = -2.9f;
            }

            transform.position = movePoint;
            //gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        else if (isUp == true)
        {
            endPos = new Vector3(6, 3.39f, 0);
            gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, (25 + speed) * Time.deltaTime);
            float objPosition_y = gameObject.GetComponent<Transform>().position.y;

            if (objPosition_x > 5 && 3.29f < objPosition_y && objPosition_y <3.49f)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 0; // so that the box layer goes below the shelf

                dust.transform.position = new Vector3(endPos.x-0.04f, 3.72f, 0);
                StartCoroutine("DustEffect");
                isUp = false;
            }
        }

        else if (isDown == true)
        {
            endPos = new Vector3(5.5f, -1.46f, 0);
            gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, (25 + speed) * Time.deltaTime);
            float objPosition_y = gameObject.GetComponent<Transform>().position.y;

            if (objPosition_x > 5 && -1.56f < objPosition_y && objPosition_y < -1.36f)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 0;  // so that box layer goes below the conveyor belt

                dust.transform.position = new Vector3(endPos.x+0.03f, -2.1f, 0);
                StartCoroutine("DustEffect");
                isDown = false;
            }
        }

        else
        {
            if (objPosition_x < 1.25f || objPosition_x >= 3.6f)
            {
                gameObject.GetComponent<Transform>().position += new Vector3(speed, 0, 0);
            }
            else
            {
                gameObject.GetComponent<Transform>().position += new Vector3(0.01f, -(speed + 0.02f), 0);
            }
        }
    }

    //dust on when the box is placed on the shelf/belt
    IEnumerator DustEffect()
    {
        dust.GetComponent<SpriteRenderer>().sprite = dustSpr;
        //PutBoxDown@@@@
        if (effectvolume != 0)
            AudioSource.PlayClipAtPoint(PutBoxDown, volVector);
        yield return new WaitForSeconds(0.05f);
        dust.GetComponent<SpriteRenderer>().sprite = null;
    }

    void ChooseSprite()
    {
        //choosing which object is in the box
        int i = Random.Range(0, 100);
        Sprite setSprite = catSpr;

        if (0 <= i && i < 48) //Cat
        {
            setSprite = catSpr;
        }
        else if (48 <= i && i < 96) //Toy
        {
            int toy_index = i % 3;
            setSprite = toySpr[toy_index];
        }
        else //Present
        {
            setSprite = presentSpr;
        }

        this.GetComponent<SpriteRenderer>().sprite = setSprite;
    }

    //for the cat to walk (changing sprite constantly)
    IEnumerator WalkingCat()
    {
        //while (true)
        //{
            walkindex = (walkindex + 1) % 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = catWalking[walkindex];
            yield return new WaitForSeconds(0.45f);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "Spawning":
                if (mySprite == boxSpr)
                {
                    GameObject.Find("Warehouse").GetComponent<SpawnBox>().CallBox();
                }
                break;

            case "BoxTo":
                if (mySprite == boxSpr)
                {
                    ChooseSprite();
                }
                break;

            case "ToyTo":
                if (timer > 30)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (mySprite == toySpr[i])
                        {
                            int fake_num = Random.Range(0, 3);//0.33 chance of toy changing to fake

                            if (fake_num == 0) //sprite == fake
                            {
                                gameObject.GetComponent<SpriteRenderer>().sprite = fakeSpr[i];
                                //CatCrying@@@@
                                if (effectvolume != 0)
                                    AudioSource.PlayClipAtPoint(CatCrying, volVector);
                            }
                        }
                    }
                }
                break;

            case "Falling":
                if (ableDrag == true)
                {
                    break;
                }
                if (mySprite == catSpr)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = catFallSpr;
                }
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == fakeSpr[i])
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = fakeFallSpr[i];
                        break;
                    }
                }
                break;

            case "Fail":
                if (mySprite == catFallSpr)
                {
                    GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
                }
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == fakeFallSpr[i])
                    {
                        GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
                        break;
                    }

                    if (mySprite == toySpr[i])
                    {
                        GameObject.Find("Main Camera").GetComponent<TotalManager_3>().ToyEnd();
                        break;
                    }
                }
                break;

            case "CatWalk":
                if (mySprite == catSpr)
                {
                    StartCoroutine("WalkingCat");
                }
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == fakeSpr[i])
                    {
                        StartCoroutine("WalkingCat");
                        break;
                    }

                    if (mySprite == toySpr[i])
                    {
                        GameObject.Find("Main Camera").GetComponent<TotalManager_3>().ToyEnd();
                    }
                }
                break;

            case "Upward":
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == catWalking[i % 2])
                    {
                        this.transform.gameObject.SetActive(false);
                        StopCoroutine("WalkingCat");
                        GameObject.Find("Warehouse").GetComponent<SpawnBox>().OrganizeBox(gameObject);
                        break;
                    }
                }
                break;

            case "Downward":
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == toySpr[i])
                    {
                        this.transform.gameObject.SetActive(false);
                        GameObject.Find("Warehouse").GetComponent<SpawnBox>().OrganizeBox(gameObject);
                        break;
                    }
                }
                break;

            case "OnBelt":
                if (mySprite == catFallSpr || mySprite == catSpr)
                {
                    GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
                }
                for (int i = 0; i < 3; i++)
                {
                    if (mySprite == fakeFallSpr[i] || mySprite == fakeSpr[i])
                    {
                        GameObject.Find("Main Camera").GetComponent<TotalManager_3>().CatEnd();
                        break;
                    }
                }
                break;
        }
    }

    //when the mouse is clicked 
    void OnMouseDown()
    {
            //range that object can be moved
            if (gameObject.GetComponent<Transform>().position.x > -3.15f && gameObject.GetComponent<Transform>().position.x < 0.8f)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 4;  // so that box layer goes upward the conveyor belt
                ableDrag = true;
                mousePosOn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);   //get position of the mouse
            }
       
    }

    //moving box to upper or down coneyor belt according to the direction of the swipe
    void OnMouseUp()
    {
        if (mySprite == presentSpr)
        {
            // organize the present box
            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            ableDrag = false;
            this.gameObject.SetActive(false);

            GameObject.Find("Warehouse").GetComponent<SpawnBox>().OrganizeBox(this.gameObject);
            GameObject.Find("Manager").GetComponent<UIManager>().PresentPlus();
            //PresentClicking@@@@
            if (effectvolume != 0)
                AudioSource.PlayClipAtPoint(PresentClicking, volVector);
        }

        else
        {
            Vector3 mousePosOff = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);   //get position of the mouse

            Vector3 direction = mousePosOff - mousePosOn;

            //range that object can be moved
            if (ableDrag == true)
            {
                if (direction.y > 0)    //swipe upward
                {
                    isUp = true;
                }

                else //swipe downward
                {
                    isDown = true;
                }

                ableDrag = false;
            }
        }
    }

    public void SetBool()
    {
        isUp = false;
        isDown = false;
        ableDrag = false;
    }
}
