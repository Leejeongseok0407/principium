﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crotch : MonsterHaviour
{
    [Header("아귀몹 정보")]
    [Tooltip("쿨타임 \n (애니메이션 재생 시간보다 길어야함)")]
    [SerializeField] float attackCoolTime;
    [Tooltip("입 닫는 시간")]
    [SerializeField] float attackDelayTime;
    [SerializeField] Animator ani = null;
    bool coolTime;
   
   private void Start()
    {
        type = 0;
        coolTime = true;
    }

    override protected bool CheckCoolTime()
    {
        if (target.GetComponent<PlayerBehaviour>().ReturnNoDmgTime())
            return false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        return coolTime;
    }


    override protected void delay()
    {

    }

    override protected void DoSkill()
    {

        StartCoroutine(AttackDelay());
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        float times = 0;
        Debug.Log("Start Attack");
        ani.SetTrigger("Attack");
        while (attackDelayTime > times)
        {
            times += Time.deltaTime;
            yield return null;
        }
        Debug.Log("End Attack");
        GetComponent<BoxCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().isTrigger = true;
        ani.SetTrigger("Default");
        Debug.Log("Default");
    }






    IEnumerator AttackDelay() {
        float times = 0;
        coolTime = false;
        Debug.Log("Start Delay");
        while (attackCoolTime > times)
        {
            times += Time.deltaTime;
            yield return null;
        }
        Debug.Log("End Delay");
        coolTime = true;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Skill();
        }
    }

}
