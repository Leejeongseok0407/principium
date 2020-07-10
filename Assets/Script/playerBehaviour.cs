using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] int playerMaxHp;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpPower = 10;
    [SerializeField] int dashPower = 20;
    [SerializeField] int skillTime = 1;
    [SerializeField] int maxJumpCount = 1;
    [SerializeField] LayerMask maskGround;
    [SerializeField] Animator ani;
    Rigidbody2D rigidbody;
    Vector2 gravity2D;
    int jumpCount;
    int hp;

    bool gravityOn = true;
    bool isGrounded = false;
    //이걸 키면 모든 몬스터 멈추고 플레이어만 죽게 설정
    bool isDead = false;

    float keyHorizontal;
    float keyVertical;

    // Start is called before the first frame update
    void Start()
    {
        hp = playerMaxHp;
        jumpCount = maxJumpCount;
        gravity2D = Physics2D.gravity;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal은 정수만 받게함
        keyHorizontal = Input.GetAxisRaw("Horizontal");
        keyVertical = Input.GetAxis("Vertical");
        OutRay();
        Jump();
    }
    void FixedUpdate()
    {

        Debug.Log(keyHorizontal);
        Move();
        if (Input.GetButtonDown("SkillKey"))
        {
            PlayerSkill(1);
        }
    }


    //플레이어 스킬을 1~7까지 만드는게 좋을듯.
    //아니면 플레이어 스킬을 2~3가지 넣어서 한번에 2~3가지 실행
    //이건 기획을봐야함.
    void PlayerSkill(int skillNum)
    {
        //대쉬 스킬
        if (skillNum == 0)
        {
            //대쉬 파워만큼 앞으로 이동시킴
            transform.Translate(Vector3.right * dashPower * keyHorizontal, Space.World);
        }
        //부유 스킬
        if (skillNum == 1)
        {
            //중력값을 0으로 초기화 해주고, 가속도를 제거한 뒤에, bool값을 통해 스킬이 켜짐을 알려줌
            Physics2D.gravity = Vector2.zero;
            rigidbody.velocity = new Vector2(0, 0);
            gravityOn = false;
            ani.SetBool("isFly", true);
            ani.SetBool("isGround", false);
            //SkillTime뒤에 중력다시줌
            Invoke("ReturnGravity", skillTime);
        }


    }

    void ReturnGravity()
    {
        Physics2D.gravity = gravity2D;
        gravityOn = true;
        ani.SetBool("isFly", false);
    }

    void Jump() {
        //gravity가 켜져있으면 점프만
        if (gravityOn)
        {
            //키메니저에 있는 input에서 Jump input실행
            if (Input.GetButtonDown("Jump"))
            {
                ani.SetBool("isJump", true);
                ani.SetBool("isGround", false);
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
    }

    void Move()
    {
        //좌우 이동i
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);

        // 0이면 사라짐
        if (keyHorizontal != 0)
        {
            Flip();
            ani.SetBool("isWalk", true);
        }
        //키입력 없을때
        if (keyHorizontal == 0)
            ani.SetBool("isWalk", false);

        //상하 이동
        //그래비티가 꺼져있을때
        if (!gravityOn)
        {
            transform.Translate(Vector3.up * jumpPower * Time.smoothDeltaTime * keyVertical, Space.World);

        }


    }

    void IsDie() {
        if (hp == 0) {
            if (!isDead)
                Die();
        }
    }

    //죽었을때 기능 추가
    void Die() {

    }

    void OutRay()
    {
        //레이케스트 사용 
        //Physics2D.Raycase(시작위치(쏘는),방향,끝나는위치,마스크)
        if (Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y + 0.1f, maskGround))
        {
            //레이 닿을때 쏴줘서 육안으로 식별 가능하게 해줌.
            Debug.DrawRay(transform.position, Vector2.down, Color.blue, 1f);
            //Ground에 닿으면 isGround는 true
            isGrounded = true;
            //Ground에 닿으면 점프횟수가 max치로로 초기화됨
            jumpCount = maxJumpCount;
            //점프 하지 않는다는걸 알려줌
            ani.SetBool("isJump", false);
            ani.SetBool("isGround", true);
        }
    }

    void Flip(){
        //현재의 크기를 받아오고 키입력한 방향으로 보게 설정함
        Vector3 theScale = transform.localScale;
        theScale.x = keyHorizontal;
        transform.localScale = theScale;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            //player hp감소 혹은 죽음 넣기
            print("몬스터와 부딛힘");
        }
    }
}
