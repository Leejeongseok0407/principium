using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobS : MonoBehaviour
{
    [SerializeField] float MobSpeed = 1;
    [SerializeField] bool IsMobJump;
    [SerializeField] bool IsMobTurn;
    [SerializeField] bool MobMove = true;
    [SerializeField] float MobJumpP = 5f;
    [SerializeField] Rigidbody2D MobRB;
    [SerializeField] float MoveDirection = 1;
    [SerializeField] Transform MobTR;
    int layerMaskO = 1 << 8;
        //LayerMask.NameToLayer("Object")가 8번임 왜 넣으면 안되는지 모르겠음.

    private void Awake()
    {

    }

    void Start()
    {

    }

    private void Update()
    {
        //단 몹무브 멈추면 관성으로 인해 떨어질 경우 있음.
        if (MobMove == true)
        {
            Movef();
            if (IsMobTurn == true)
                TurnRayf();
            if (IsMobJump == true)
                JunpRayf();
        }
    }

    void TurnRayf()
    {
        //frontBec는 내가 앞에 쏴줄 레이어 즉 충돌 판정할곳 정해줌 
        Vector2 frontBec = new Vector2(MobRB.position.x + MoveDirection * MobTR.localScale.x / 5, MobRB.position.y - 0.5f);

        //Debug.DrawRay(x,y,z,r)x는 기준점, y는 방향 z는 색,r은 유지시간
        Debug.DrawRay(frontBec, Vector3.down, Color.red, 1);

        //Physics2D.Raycast(x,y,r) 변수는 위와 동일
        RaycastHit2D rayHit = Physics2D.Raycast(frontBec, Vector3.down, 1, layerMaskO);

        if (rayHit.collider == null)
        {
            turnf();
            Debug.Log("turn");
        }
    }

    void JunpRayf()
    {
        //frontBec는 내가 앞에 쏴줄 레이어 즉 충돌 판정할곳 정해줌 
        Vector2 front = new Vector2(MobRB.position.x + MoveDirection * MobTR.localScale.x / 3, MobRB.position.y+1f);

        Vector2 debugD = new Vector2(0, -1);
        //Debug.DrawRay(x,y,z,r)x는 기준점, y는 방향 z는 색,r은 유지시간
        Debug.DrawRay(front, debugD, Color.red, 1);

        //Physics2D.Raycast(x,y,r) 변수는 위와 동일
        RaycastHit2D rayHit = Physics2D.Raycast(front, debugD , 1, layerMaskO);

        if (rayHit.collider != null)
        {
            junpf();
            Debug.Log("jump");
        }
    }

    //움직이는 함수임 velocity(Rb의 속도)를 이용하여 방향을 바꿔줌. 단순히 이동만 생각하여서 MoveDirection을 직접적으로 변경 하지 않음. 
    //rotation을 이용해 몹이 보는 방향을 변경해줌.
    void Movef() {
        if (MoveDirection == 1)
        {
            MobRB.velocity = new Vector2(MobSpeed, MobRB.velocity.y);
            MobTR.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (MoveDirection == -1)
        {
            MobRB.velocity = new Vector2(-MobSpeed, MobRB.velocity.y);
            MobTR.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }

    //MoveDirection을 바꿔주는 함수.
    void turnf()
    {
        MoveDirection *= -1;
    }
    //점프하는 함수이며 장애물의 크기에 따라 점프 파워 혹은 뒤에 붙은 상수를 변경 하면 됨.
    void junpf()
    {
            MobRB.AddForce(Vector2.up * MobJumpP * 5);
        IsMobJump = false;
        StartCoroutine(stopJump());
    }

    //코루틴을 이용해 1초의 대기 시간을 준뒤 점프를 풀어주었음. 이를 통해 연속 점프하는 현상이 해결됨.
    IEnumerator stopJump()
    {
        yield return new WaitForSeconds(1f);
        IsMobJump = true;
    }
}