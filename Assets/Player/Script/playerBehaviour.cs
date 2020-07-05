using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerBehaviour : MonoBehaviour
{
    [SerializeField] int playerHp;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpPower = 10;
    [SerializeField] int dashPower = 20;
    [SerializeField] int SkillTime = 1;
    [SerializeField] int maxJumpCount = 1;
    [SerializeField] LayerMask maskGround;
    Rigidbody2D rigidbody;
    Vector2 gravity2D;
    int jumpCount;
    bool gravityOn = true;
    bool isGrounded = false;
    float keyHorizontal;
    float keyVertical;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
        gravity2D = Physics2D.gravity;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        keyHorizontal = Input.GetAxis("Horizontal");
        keyVertical = Input.GetAxis("Vertical");
        outRay();
        Move();
    }
    void FixedUpdate()
    {

        Debug.Log(jumpCount);
        Debug.Log(isGrounded);
        if (Input.GetButtonDown("SkillKey"))
        {
            PlayerSkill(1);
        }
    }


    //플레이어 스킬을 1~7까지 만드는게 좋을듯.
    //아니면 플레이어 스킬을 2~3가지 넣어서 한번에 2~3가지 실행
    //이건 기획을봐야함.
    void PlayerSkill(int SkillNum)
    {
        //대쉬 스킬
        if (SkillNum == 0)
        {
            transform.Translate(Vector3.right * dashPower * keyHorizontal, Space.World);
        }
        //부유 스킬
        if (SkillNum == 1)
        {
            Physics2D.gravity = Vector2.zero;
            gravityOn = false;
            Invoke("returnGravity", SkillTime);
        }


    }

    void returnGravity()
    {
        Physics2D.gravity = gravity2D;
        gravityOn = true;
    }

    void Move()
    {
        //좌우 이동
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);

        //상하 이동
        //gravity가 켜져있으면 점프만
        if (gravityOn)
        {
            //키메니저에 있는 input에서 Jump input실행
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpCount > 0)
                {
                    //한번 점프 했을때 
                    if (!isGrounded)
                    {
                        //가속도를 초기화 해줘서 점프를 낮게 하거나 안하는 것을 막아줌
                        rigidbody.velocity = new Vector2(0, 0);
                    }
                    rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    isGrounded = false;
                    jumpCount--;
                }
            }
        }
        else
        {
            transform.Translate(Vector3.up * jumpPower * Time.smoothDeltaTime * keyVertical, Space.World);

        }


    }

    void outRay() {
        //레이케스트 사용 
        //Physics2D.Raycase(시작위치(쏘는),방향,끝나는위치,마스크)
        if (Physics2D.Raycast(transform.position , Vector2.down, transform.localScale.y, maskGround))
        {
            //레이 닿을때 쏴줘서 육안으로 식별 가능하게 해줌.
            Debug.DrawRay(transform.position, Vector2.down, Color.blue, 1f);
            //Ground에 닿으면 isGround는 true
            isGrounded = true;
            //Ground에 닿으면 점프횟수가 max치로로 초기화됨
            jumpCount = maxJumpCount;       
        }
    }

}
