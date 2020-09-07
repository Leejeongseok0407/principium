using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{

    [Header("플레이어 기본 정보")]
    [SerializeField] int playerMaxHp = 5;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpPower = 20;
    [Tooltip("0.1초 동안 움직일 거리이니 수치 많은게 정상 ")]
    [SerializeField] int dashSpeed = 30;
    [Tooltip("스킬 지속지간 (현재 부유만 참고)")]
    [SerializeField] int skillTime = 5;
    [SerializeField] int maxJumpCount = 2;
    [SerializeField] int noDmgTime = 3;
    [SerializeField] float bounceWidth = 5;
    [SerializeField] float bounceHight = 10;

    [Header("플레이어 가 판단하는 레이어들")]
    [SerializeField] LayerMask maskGround = 1;
    [SerializeField] LayerMask maskTransmissionGround = 2;


    [Header("스킬 관련")]
    [Tooltip("0~6 스킬 번호 맞게 할당해줌")]
    [SerializeField] float[] skillCoolTime = new float[7];
    [SerializeField] bool[] isSkillCanActive = new bool[7];

    [Header("에니메이션")]
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] Animator ani = null;

    [Header("UI")]
    [SerializeField] GameObject UiManager;


    Rigidbody2D playerRigidBody;
    Vector2 gravity2D;
    float dashTime;
    int jumpCount;
    int dashDirction = 0;
    int hp;
    int playerLookDirction = 0;

    bool isGravityOn = true;
    //이걸 키면 모든 몬스터 멈추고 플레이어만 죽게 설정
    bool isDead = false;
    //무적인지 판단
    bool isNoDmgTime = false;

    float keyHorizontal;
    float keyVertical;
    float maxDashTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        hp = playerMaxHp;
        dashTime = maxDashTime;
        jumpCount = maxJumpCount;
        gravity2D = Physics2D.gravity;
        playerRigidBody = GetComponent<Rigidbody2D>();
        GetSkill();
        
    }

    // Update is called once per frame
    void Update()
    {
        //죽었을 경우 입력 안받음
        if (isDead == true)
            return;
        //Horizontal은 정수만 받게함
        RayCheckGorund();
        Jump();
        PlayerSkill();
    }
    void FixedUpdate()
    {
        //죽었을 경우 입력 안받음
        if (isDead == true)
            return;
        //인풋메니져 참고
        keyHorizontal = Input.GetAxisRaw("Horizontal");
        keyVertical = Input.GetAxis("Vertical");
        Move();

    }

    void GetSkill() {
        if (PlayerPrefs.HasKey("SkillIndex") == true)
            for (int i = 0; i < PlayerPrefs.GetInt("SkillIndex"); i++)
                isSkillCanActive[i] = true;
        else
            return;
    }

    int InputSkillNum()
    {
        if (Input.GetButtonDown("SkillKey1"))
            return 0;
        if (Input.GetButtonDown("SkillKey2"))
            return 1;
        return -1;
    }

    //플레이어 스킬을 1~7까지 만드는게 좋을듯.
    //아니면 플레이어 스킬을 2~3가지 넣어서 한번에 2~3가지 실행
    //이건 기획을봐야함.
    void PlayerSkill()
    {
        int skillNum = InputSkillNum();
        //대쉬 스킬
        if (skillNum == 0)
        {
            if (isSkillCanActive[0] == true)
            {
                StartCoroutine(Dash());
                StartCoroutine(CoolTime(skillNum));
//              ani.SetBool("isDash", true);
            }
        }
        
        //부유 스킬
        if (skillNum == 1)
        {
            if (isSkillCanActive[1] == true)
            {
                StartCoroutine(CoolTime(skillNum));
                StartCoroutine(Fly());
                
            }
        }
    }



    void Jump()
    {
        //gravity가 켜져있으면 점프만
        if (isGravityOn == true)
        {
            //키메니저에 있는 input에서 Jump input실행
            if (Input.GetButtonDown("Jump"))
            {
                if (Input.GetKey(KeyCode.DownArrow) &&
                    Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y + 0.1f, maskTransmissionGround))
                {
                    DownJump();
                }
                else if(Input.GetKey(KeyCode.DownArrow) == false)
                {
                    if (jumpCount > 0)
                    {
                        //한번 점프 했을때 
                        if (playerRigidBody.velocity != Vector2.zero)
                        {
                            //가속도를 초기화 해줘서 점프를 낮게 하거나 안하는 것을 막아줌
                            playerRigidBody.velocity = Vector2.zero;
                        }
                        playerRigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                        jumpCount--;
                        ani.SetBool("isJump", true);
                        ani.SetBool("isGround", false);
                    }
                }
            }

        }
    }

    void DownJump() {
        transform.Translate(Vector3.down * 0.01f, Space.World);
        GetComponent<BoxCollider2D>().isTrigger = true;
        ani.SetBool("isJump", true);
        ani.SetBool("isGround", false);
    }

    void Move()
    {
        //좌우 이동i
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);

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
        if (!isGravityOn)
        {
            transform.Translate(Vector3.up * speed * Time.smoothDeltaTime * keyVertical, Space.World);

        }


    }

    void IsDie()
    {
        if (!isDead)
            Die();
    }

    //죽었을때
    //차후 기능 추가
    //UI상 플레이어가 사망하고 시간 멈추는거 구현 부탁드립니다
    void Die()
    {
        isDead = true;
        Debug.Log("die");
        ani.SetTrigger("DieTrigger");
        UiManager.GetComponent<Hearts>().GameOver();
        noDmgTime = 0;

    }

    void RayCheckGorund()
    {
        if (GetComponent<BoxCollider2D>().isTrigger == false)
        {
            //레이케스트 사용 
            //Physics2D.Raycase(시작위치(쏘는),방향,끝나는위치,마스크)
            if (Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y + 0.1f, maskGround) ||
                Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y + 0.1f, maskTransmissionGround))
            {
                //레이 닿을때 쏴줘서 육안으로 식별 가능하게 해줌.
                Debug.DrawRay(transform.position, Vector2.down, Color.blue, 1f);
                //Ground에 닿으면 점프횟수가 max치로로 초기화됨
                jumpCount = maxJumpCount;
                //점프 하지 않는다는걸 알려줌
                ani.SetBool("isJump", false);
                ani.SetBool("isGround", true);
            }
        }
    }

    void Flip()
    {
        if (keyHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            playerLookDirction = 1;
        }
        if (keyHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            playerLookDirction = -1;
        }
        
    }

    public bool ReturnNoDmgTime() {
        return isNoDmgTime;
    }

    public int ReturnPlayerHp() {
        return playerMaxHp;
    }

    //트리거
    //몬스터는 Collision에 닿을 경우 튕겨 나가고
    //오브젝트는 trigger에 닿으면 튕겨 나감
    //몬스터는 실체가 있고 오브젝트는 실체가 없다고 생각하여 이렇게 구상 하였음.

    void OnCollisionStay2D(Collision2D other)
    {
        if (isDead)
            return;
        if (isNoDmgTime == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                Debug.Log("outchi");
                Vector2 attackedVelocity = Vector2.zero;
                float dir = (transform.position.x - other.gameObject.transform.position.x)
                              / Mathf.Abs(other.gameObject.transform.position.x - transform.position.x);
                Debug.Log(dir);
                playerRigidBody.velocity = Vector2.zero;
                attackedVelocity = new Vector2(dir * bounceWidth, bounceHight);
                playerRigidBody.AddForce(attackedVelocity, ForceMode2D.Impulse);
                hp -= other.gameObject.GetComponent<MonsterHaviour>().dmg;
                UiManager.GetComponent<Hearts>().HPUpdate(hp);
                StartCoroutine("NoDmgTime");
                Debug.Log("몬스터와 부딛힘 hp : " + hp);
            }
        }
        if (hp <= 0)
            IsDie();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //벽 통과 스크립트
        if (other.gameObject.layer == LayerMask.NameToLayer("TransmissionGround"))
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TransmissionGround"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead)
            return;
        if (isNoDmgTime == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bullit"))
            {
                Vector2 attackedVelocity = Vector2.zero;
                float dir = other.gameObject.GetComponent<Bullit>().CallDirctionX()
                    / Mathf.Abs(other.gameObject.GetComponent<Bullit>().CallDirctionX());

                playerRigidBody.velocity = new Vector2(0, 0);
                attackedVelocity = new Vector2(dir * bounceWidth, bounceHight);

                playerRigidBody.AddForce(attackedVelocity, ForceMode2D.Impulse);

                hp -= other.gameObject.GetComponent<Bullit>().CallBullitDmg();
                StartCoroutine("NoDmgTime");
                UiManager.GetComponent<Hearts>().HPUpdate(hp);
                Debug.Log("총알이랑 부딛힘 hp : " + hp);
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Impediments"))
            {
                Vector2 attackedVelocity = Vector2.zero;
                float dir = -playerLookDirction;

                playerRigidBody.velocity = new Vector2(0, 0);
                attackedVelocity = new Vector2(dir * bounceWidth, bounceHight);

                playerRigidBody.AddForce(attackedVelocity, ForceMode2D.Impulse);

                hp -= other.gameObject.GetComponent<Impediments>().CallDmg();
                StartCoroutine(NoDmgTime());
                UiManager.GetComponent<Hearts>().HPUpdate(hp);
                Debug.Log("방해물이랑 부딛힘 hp : " + hp);
            }
        }
        if (hp <= 0)
            IsDie();
    }
    //코루틴에 사용되는 IEnumerator
    IEnumerator NoDmgTime()
    {
        isNoDmgTime = true;
        Debug.Log("코루틴중");
        float countTime = 0;
        while (countTime < noDmgTime)
            {
                if (countTime % 0.2 < 0.1f)
                    spriteRenderer.color = new Color32(255, 255, 255, 90);
                else
                    spriteRenderer.color = new Color32(255, 255, 255, 180);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitForEndOfFrame();
                countTime += 0.1f;
            }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        isNoDmgTime = false;
        yield return null;
    }

    IEnumerator Dash()
    {
        dashDirction = (int)keyHorizontal;
        Debug.Log("코루틴중");
        while (true)
        {
            if (dashTime <= 0)
            {
                playerRigidBody.velocity = Vector2.zero;
                dashTime = maxDashTime;
                dashDirction = 0;
                break;
            }
            else
            {
                dashTime -= Time.deltaTime;
                playerRigidBody.velocity = Vector2.right * dashDirction * dashSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator Fly()
    {
        //중력값을 0으로 초기화 해주고, 가속도를 제거한 뒤에, bool값을 통해 스킬이 켜짐을 알려줌
        Physics2D.gravity = Vector2.zero;
        playerRigidBody.velocity = Vector2.zero;
        isGravityOn = false;
        ani.SetBool("isFly", true);
        ani.SetBool("isGround", false);

        //SkillTime뒤에 중력다시줌
        yield return new WaitForSeconds(skillTime);
        Physics2D.gravity = gravity2D;
        isGravityOn = true;
        ani.SetBool("isFly", false);

    }

    IEnumerator CoolTime(int index)
    {
        float times = 0;
        isSkillCanActive[index] = false;
       

        while (skillCoolTime[index] > times)
        {
            times += Time.deltaTime;
            //img.fillAmount = (times / coolTime);
            yield return null;
        }

        isSkillCanActive[index] = true;
    }

}
