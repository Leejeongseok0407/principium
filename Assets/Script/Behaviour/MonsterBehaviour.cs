using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHaviour : MonoBehaviour
{
    [SerializeField] public int dmg = 1;
    [Tooltip("0. 고정 " +
        "\n 1. 바닥체크(턴)" +
        "\n 2. 웨이포인트 따라감" +
        "\n 3. 플라이(A*있어야함)")]
    [SerializeField] int type = 0;
    //   [SerializeField] int hp = 1;

    [SerializeField] float jumpDelay = 1;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float direction = 1;

    [SerializeField] bool isJumpRay = false;
    [SerializeField] bool isTurnRay = false;
    [SerializeField] bool isTrackingPlayer = false;
    [SerializeField] bool isCanDetectTarget = false;
    [SerializeField] bool isMobMove = true;

    [SerializeField] Rigidbody2D mobRB = null;
    [SerializeField] Transform mobTR = null;

    [Tooltip("0이 왼쪽 1이 오른쪽, 몬스터는 0부터 시작함")]
    GameObject[] wayPoint = new GameObject[2];
    [SerializeField] GameObject target = null;
    int layerMaskO = 1 << 8;
    bool isInWayPoint = false;
    bool isNowLookAtPlayer = false;
    Vector3 targetPosition;
    Vector3 dirctoinV;


    // Start is called before the first frame update
    void Awake()
    {
        LookForward();
    }

    // 상속을 위한 클래스 이므로 아래에 전부 오버라이딩 해줘야함.
    private void Update()
    {
        if (isMobMove == true)
        {
            Patten();
        }

        Debug.Log(dirctoinV);
    }

    //virtual은 상속을 위한 함수로 새로운 스크립트에서 오버라이딩 해줘야함.
    public virtual void Skill()
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
                isMobMove = false;
                LookForward();
                break;
            //바닥을 만나면 턴 하는 패턴
            case 1:
                Move();
                if (isNowLookAtPlayer == false)
                {
                    LookForward();
                }
                if (isTurnRay == true)
                    TurnRay();
                if (isJumpRay == true)
                    JunpRay();

                break;

            //웨이포인트 따라가는 패턴
            case 2:
                if (isTrackingPlayer == true)
                {
                    if (isNowLookAtPlayer == true)
                    {
                        LookTarget();
                        TrackingPlayer();
                    }
                    else if (isInWayPoint == true)
                    {
                        MoveToWayPoint();
                        LookForward();
                    }
                    else if (isInWayPoint == false)
                    {
                        BackToWayPoint();
                        LookForward();
                    }
                }
                else if (isTrackingPlayer == false)
                {
                    MoveToWayPoint();
                    LookForward();
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
        Debug.Log("BackToWayPoint");
    }

    //wayPoint를 왕복하는 함수.
    void MoveToWayPoint()
    {
        Debug.Log("MoveToWayPoint");
        dirctoinV = new Vector3(direction, 0, 0);
        transform.Translate(dirctoinV * speed * Time.smoothDeltaTime, Space.World);
        //방향 바꿔주는 함수
        if ((wayPoint[0].transform.position.x >= transform.position.x || transform.position.x >= wayPoint[1].transform.position.x) && isInWayPoint == true)
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
        isJumpRay = false;
        StartCoroutine(stopJump());
    }


    //<콜라이더 충돌 체크>

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isCanDetectTarget == true)
        {
            //player hp감소 혹은 죽음 넣기
            isNowLookAtPlayer = true;
            LookTarget();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // if (transform.position.x != wayPoint[0].transform.position.x)
            isInWayPoint = false;
            isNowLookAtPlayer = false;
            //                LookForward();
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
        isJumpRay = true;
    }
}
