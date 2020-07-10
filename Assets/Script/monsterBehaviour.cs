using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHaviour : MonoBehaviour
{
    [Tooltip("1은 정찰만, 2는 정찰 + 점프")]
    [SerializeField] int type;
    [SerializeField] int hp;
    //데미지 있어야함?
    [SerializeField] int dmg;
    [SerializeField] int jumpDelay;

    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float direction = 1;

    [SerializeField] bool canJump;
    [SerializeField] bool canTurn;
    [SerializeField] protected bool mobMove = true;

    [SerializeField] Rigidbody2D mobRB;
    [SerializeField] Transform mobTR;
    int layerMaskO = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        /*
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        */
    }

    // Update is called once per frame
    private void Update()
    {
        //단 몹무브 멈추면 관성으로 인해 떨어질 경우 있음.
        if (mobMove == true)
        {
            Patten();
        }
    }

    
        


    // 이동 모션을 의미함
    public void Patten()
    {
        {
            switch (type)
            {
                //몹무브
                case 1:
                    Move();
                    if (canTurn == true)
                        TurnRay();
                    if (canJump == true)
                        JunpRay();
                    break;
                //플라이 넣기
                case 2:
                    canJump = false;
                    //패트롤
                    //추적 넣기
                    break;


            }
        }
    }

    //<움직임에 관한 함수>

    //움직이는 함수임 velocity(Rb의 속도)를 이용하여 방향을 바꿔줌. 단순히 이동만 생각하여서 MoveDirection을 직접적으로 변경 하지 않음. 
    //rotation을 이용해 몹이 보는 방향을 변경해줌.
    void Move()
    {
        if (direction == 1)
        {
            mobRB.velocity = new Vector2(speed, mobRB.velocity.y);
            mobTR.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (direction == -1)
        {
            mobRB.velocity = new Vector2(-speed, mobRB.velocity.y);
            mobTR.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    //MoveDirection을 바꿔주는 함수.
    void Turn()
    {
        direction *= -1;
    }

    //점프하는 함수이며 장애물의 크기에 따라 점프 파워 혹은 뒤에 붙은 상수를 변경 하면 됨.
    void Jump()
    {
        mobRB.AddForce(Vector2.up * jumpPower * 5);
        canJump = false;
        StartCoroutine(stopJump());
    }


    //<콜라이더 충돌 체크>

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //player hp감소 혹은 죽음 넣기
            print("플레이어와 부딛힘");
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
        yield return new WaitForSeconds(1f);
        canJump = true;
    }
}
