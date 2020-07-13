using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHaviour : MonoBehaviour
{
    [SerializeField] public int dmg = 1;
    [Tooltip("0. 고정 " +
        "\n 1. 바닥체크(턴, 점프)" +
        "\n 2. 웨이포인트 따라감" +
        "\n 3. 플라이(A*있어야함)")]
    [SerializeField] int type = 0;
    //   [SerializeField] int hp = 1;


    [SerializeField] protected float jumpDelay = 1;
    [SerializeField] protected float speed = 1;
    [SerializeField] protected float jumpPower = 5f;
    [SerializeField] protected float direction = 1;

    [SerializeField] bool isCanJumpRay = false;
    [SerializeField] bool isCanTurnRay = false;
    [SerializeField] bool isCanTrackingPlayer = false;
    [SerializeField] bool isCanDetectTarget = false;
    [SerializeField] bool isCanMobMove = true;
    bool isInWayPoint = false;
    bool isLookAtPlayer = false;
    bool isCanMobMoveTmp;
    [SerializeField] Rigidbody2D mobRB = null;
    [SerializeField] Transform mobTR = null;

    [Tooltip("0이 왼쪽 1이 오른쪽, 몬스터는 0부터 시작함")]
    [SerializeField] GameObject[] wayPoint = new GameObject[2];

    [SerializeField] GameObject target = null;
    protected int layerMaskO = 1 << 8;
    protected Vector3 targetPosition;
    protected Vector3 dirctoinV;


    // Start is called before the first frame update
    void Awake()
    {
        LookForward();
        isCanMobMoveTmp = isCanMobMove;
    }

    // 상속을 위한 클래스 이므로 아래에 전부 오버라이딩 해줘야함.
    private void Update()
    {
        if (isCanMobMove == true)
        {
            Patten();
        }
        Debug.Log(dirctoinV);
    }

    //virtual은 상속을 위한 함수로 새로운 스크립트에서 오버라이딩 해줘야함.
    virtual protected void Skill()
    {

    }


    void TrackingPlayer()
    {
        //단위 벡터화
        Debug.Log("TrackingPlayer 실행중");
        dirctoinV = new Vector3((target.transform.position.x - transform.position.x) / Mathf.Abs(target.transform.position.x - transform.position.x), 0, 0);
        direction = dirctoinV.x;
        transform.Translate(dirctoinV * speed * Time.smoothDeltaTime, Space.World);

    }
    public void LookTarget()
    {
        if (target.transform.position.x >= transform.position.x)
            mobTR.rotation = Quaternion.Euler(0, 180, 0);
        else
            mobTR.rotation = Quaternion.Euler(0, 0, 0);
    }



    // 이동 모션을 의미함
    public void Patten()
    {
        switch (type)
        {
            //고정몹
            //고정몹은 몹 무브만 없애면됨
            case 0:

                if (isCanTrackingPlayer == true)
                {
                    if (isLookAtPlayer == true)
                    {
                        LookTarget();
                        TrackingPlayer();
                        isCanMobMove = true;
                    }
                    else if (isLookAtPlayer == false)
                    {
                        isCanMobMove = false;
                        LookForward();
                    }
                }
                else if (isCanTrackingPlayer == false)
                {
                    isCanMobMove = false;
                    LookForward();
                }
                break;
            //바닥을 만나면 턴 하는 패턴
            case 1:
                Move();
                if (isLookAtPlayer == false)
                {
                    LookForward();
                }
                if (isCanTurnRay == true)
                    TurnRay();
                if (isCanJumpRay == true)
                    JunpRay();

                break;

            //웨이포인트 따라가는 패턴
            case 2:
                if (isCanTrackingPlayer == true)
                {
                    if (isLookAtPlayer == true)
                    {
                        LookTarget();
                        TrackingPlayer();
                    }
                    else 
                    {
                        if (isInWayPoint == true)
                            MoveToWayPoint();
                        if (isInWayPoint == false)
                            BackToWayPoint();
                        LookForward();
                    }
                }
                else if (isCanTrackingPlayer == false)
                {
                    if (isLookAtPlayer == true)
                    {
                        LookTarget();
                    }
                    else
                    {
                        LookForward();
                    }
                    MoveToWayPoint();
                }
                break;

            //날아다니는 몹
            case 3:
                break;
        }
    }

    //<움직임에 관한 함수>

    //waypoint 로 돌아가는 함수
    void BackToWayPoint()
    {
        dirctoinV = new Vector3((wayPoint[0].transform.position.x - transform.position.x) / Mathf.Abs(transform.position.x - wayPoint[0].transform.position.x), 0, 0);
        transform.Translate(dirctoinV * speed * Time.smoothDeltaTime, Space.World);
        if (!(wayPoint[0].transform.position.x >= transform.position.x || transform.position.x >= wayPoint[1].transform.position.x))
        {
            Debug.Log("gotoway false");
            isInWayPoint = true;
        }
        else
            isInWayPoint = false;
        Debug.Log("BackToWayPoint");
    }



    //wayPoint를 왕복하는 함수.
    void MoveToWayPoint()
    {
        Debug.Log("MoveToWayPoint");
        dirctoinV = new Vector3(direction, 0, 0);
        transform.Translate(dirctoinV * speed * Time.smoothDeltaTime, Space.World);
        //방향 바꿔주는 함수
        if ((wayPoint[0].transform.position.x >= transform.position.x || transform.position.x >= wayPoint[1].transform.position.x))
        {
            Turn();
        }

    }

    //움직이는 함수임 velocity(Rb의 속도)를 이용하여 방향을 바꿔줌. 단순히 이동만 생각하여서 MoveDirection을 직접적으로 변경 하지 않음. 
    //rotation을 이용해 몹이 보는 방향을 변경해줌.
    void Move()
    {
        mobRB.velocity = new Vector2(direction * speed, mobRB.velocity.y);

    }

    //MoveDirection을 바꿔주는 함수.
    void Turn()
    {
        direction *= -1;
        Debug.Log("turn");
    }

    void LookForward()
    {
        if (dirctoinV.x > 0)
            mobTR.rotation = Quaternion.Euler(0, 180, 0);
        if (dirctoinV.x < 0)
            mobTR.rotation = Quaternion.Euler(0, 0, 0);
    }

    //점프하는 함수이며 장애물의 크기에 따라 점프 파워 혹은 뒤에 붙은 상수를 변경 하면 됨.
    void Jump()
    {
        mobRB.AddForce(Vector2.up * jumpPower * 5);
        isCanJumpRay = false;
        StartCoroutine(stopJump());
    }


    //<콜라이더 충돌 체크>

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isCanDetectTarget == true)
        {
            //player hp감소 혹은 죽음 넣기
            isLookAtPlayer = true;
            LookTarget();
            //만약 몹 안움직였을때 탐지 가능하게 할 경우
            if (isCanTrackingPlayer == true&& isCanMobMoveTmp == false)
            {
                isCanMobMove = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isInWayPoint = false;
            isLookAtPlayer = false;
            //만약 몹 안움직이는데 탐지 가능하게 할 경우
            if (isCanTrackingPlayer == true && isCanMobMoveTmp == false)
            {
                isCanMobMove = false;
            }
        }
    }

    //<레이 케스트 함수들>
    void TurnRay()
    {
        //frontBec는 내가 앞에 쏴줄 레이어 즉 충돌 판정할곳 정해줌 
        Vector2 frontBec = new Vector2(mobRB.position.x + direction * mobTR.localScale.x, mobRB.position.y - 0.5f);

        //Debug.DrawRay(x,y,z,r)x는 기준점, y는 방향 z는 색,r은 유지시간
        Debug.DrawRay(frontBec, Vector2.down, Color.red, 1);

        //Physics2D.Raycast(x,y,r) 변수는 위와 동일
        RaycastHit2D rayHit = Physics2D.Raycast(frontBec, Vector3.down, 1, layerMaskO);

        if (rayHit.collider == null)
        {
            Turn();
            Debug.Log("turn");
        }
    }

    void JunpRay()
    {
        //frontBec는 내가 앞에 쏴줄 레이어 즉 충돌 판정할곳 정해줌 
        Vector2 front = new Vector2(mobRB.position.x + direction * mobTR.localScale.x, mobRB.position.y + 1f);

        //Debug.DrawRay(x,y,z,r)x는 기준점, y는 방향 z는 색,r은 유지시간
        Debug.DrawRay(front, Vector2.down, Color.red, 1);

        //Physics2D.Raycast(x,y,r) 변수는 위와 동일
        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector2.down, 1, layerMaskO);

        if (rayHit.collider != null)
        {
            Jump();
            Debug.Log("jump");
        }
    }


    //코루틴을 이용해 1초의 대기 시간을 준뒤 점프를 풀어주었음. 이를 통해 연속 점프하는 현상이 해결됨.
    IEnumerator stopJump()
    {
        yield return new WaitForSeconds(jumpDelay);
        isCanJumpRay = true;
    }
}
