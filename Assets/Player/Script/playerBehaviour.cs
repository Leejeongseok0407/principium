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
    [SerializeField] int jumpMaxCount = 2;
    int jumpCount;
    Rigidbody2D rigidbody;
    Vector2 gravity2D;
    bool gravityOn = true;
    bool isGrounded = false;
    float keyHorizontal;
    float keyVertical;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = jumpMaxCount;
        gravity2D = Physics2D.gravity;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        keyHorizontal = Input.GetAxis("Horizontal");
        keyVertical = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        Move();

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
                Debug.Log(jumpCount);
                if (jumpCount > 0)
                {
                    rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    jumpCount--;
                }
            }
        }
        else
        {
            transform.Translate(Vector3.up * jumpPower * Time.smoothDeltaTime * keyVertical, Space.World);

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("트리거");
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("닿음");
        }
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("땅에에 닿음");
            isGrounded = true;    //Ground에 닿으면 isGround는 true
            jumpCount = 2;          //Ground에 닿으면 점프횟수가 2로 초기화됨
        }
    }
}
