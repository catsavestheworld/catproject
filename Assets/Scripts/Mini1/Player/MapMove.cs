using UnityEngine;
using System.Collections;
public class MapMove : MonoBehaviour
{
    public GameObject tile;//바닥에 사용할 GameObject(프리팹).
    //private GameObject[] tiles;
    private int tileNum;
    struct TileStruct
    {//타일 사용에 필요한 정보들을 담고있는 구조체.
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
    }
    private TileStruct[] tiles;

    private Vector3 tileCenterVec;//기준점.
    private float tileGap;//블록 길이 차이.
    private float tileEndPoint;
    private Vector3 tempVec;//임시벡터.
    private float speed;//배경초기 이동속도.
    private int lastTileNum;//현재 가장 뒤에 위치한 블록의 번호.

    void Start()
    {
        tileNum = 3;
        tileGap = 17.03f;
        tileEndPoint = -15f;
        speed = 0.05f;
        lastTileNum = 0;

        tileCenterVec = new Vector3(4.5f, 0, 10);
        CreateTiles();
    }
  
    void FixedUpdate()
    {//블록이 계속하여 일정한 (주어진)속도로 이동하도록 만듭니다.
        
        for (int i = 0; i < tileNum; i++)
        { 
            tiles[i].pos.x -= speed;

            if (tiles[i].pos.x > tileEndPoint)
            {//화면의 보이지 않는 지점으로 정해준 곳보다 더 가지 않았으면. (일반적)
                tiles[i].tf.position = tiles[i].pos;
            }
            else
            {//endPoint 넘어감 -> 가장 마지막 블록으로 위치시킴.
                tiles[i].pos = tiles[lastTileNum].pos;
                tiles[i].pos.x += tileGap;
                if (lastTileNum > i)//i보다 크다면 아직 0.5f 감소가 안 된 상황이니 추가로 0.5 감소해줌.
                {
                    tiles[i].pos.x -= 0.05f;
                }
                tiles[i].tf.position = tiles[i].pos;//실제 위치 변경.
                lastTileNum = i;//다음에 바꿀 때를 위해 마지막 블록 번호 바꿔줌.
            }
        }
    }
    void CreateTiles()//반복 사용할 타일들을 생성합니다. (오브젝트풀)
    {
        tempVec = tileCenterVec;//생성 위치 지정을 위한 최초 기준점.
        tiles = new TileStruct[tileNum];//총 3개 반복 사용.
        for (int i = 0; i < tileNum; i++)
        {//기본정보와 위치도 셋팅해줍니다.
            tiles[i].obj = Instantiate(tile, tempVec, Quaternion.identity) as GameObject;//생성.
            tiles[i].tf = tiles[i].obj.transform;
            tiles[i].pos = tiles[i].tf.position;
            tiles[i].active = true;

            tempVec.x += tileGap;//다음 블록은 tileGap만큼 플러한 위치에 만듦.
        }
        lastTileNum = 2;//처음엔 012순으로 위치.
    }

}


