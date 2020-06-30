﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    [SerializeField] int playerHp;
    [SerializeField] int speed = 10;
    [SerializeField] int jumpPower = 10;
    [SerializeField] int dashPower = 20;
    [SerializeField] int SkillTime = 1;
    Vector2 gravity2D;

    // Start is called before the first frame update
    void Start()
    {
        gravity2D = Physics2D.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerSkill(1);
        }
    }

    void PlayerMove() { 
    
    }

    //플레이어 스킬을 1~7까지 만드는게 좋을듯.
    //아니면 플레이어 스킬을 2~3가지 넣어서 한번에 2~3가지 실행
    //이건 기획을봐야함.
    void PlayerSkill(int SkillNum) {
        //대쉬 스킬
        if (SkillNum == 0){
            float keyHorizontal = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * dashPower * keyHorizontal, Space.World);
        }
        //부유 스킬
        if (SkillNum == 1) {
            Physics2D.gravity = Vector2.zero;
            Invoke("returnGravity", SkillTime);
        }
    

    }

    void returnGravity(){
        Physics2D.gravity = gravity2D;

    }

    void Move() {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);
        transform.Translate(Vector3.up * jumpPower * Time.smoothDeltaTime * keyVertical, Space.World);
    }
}
