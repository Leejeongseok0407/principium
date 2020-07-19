using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crotch : MonsterHaviour
{
    [SerializeField] float attackDelayTime;
    bool coolTime;
   
   private void Start()
    {
        type = 0;
        coolTime = true;
    }

    override protected bool CheckCoolTime()
    {
        return coolTime;
    }

    override protected void delay()
    {
    }

    override protected void DoSkill()
    {
        Attack();
        StartCoroutine(AttackDelay());
    }

    void Attack() {
        Debug.Log("Attack!");
    }

    IEnumerator AttackDelay() {
        float times = 0;
        coolTime = false;
        while (attackDelayTime > times)
        {
            times += Time.deltaTime;
            yield return null;
        }
        coolTime = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Skill();
        }
    }

    private bool IsShoot()
    {
        return true;
    }
}
