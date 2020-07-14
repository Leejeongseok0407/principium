using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonsterHaviour
{
    [SerializeField] GameObject[] bullits;
    [SerializeField] int bullitDistance;
    [SerializeField] float fireInterval = 2;
    [SerializeField] int bullitDmg;
    int bulletIndex = 0;

    private void Start()
    {
        StartCoroutine("SkillAction");
    }

    override protected bool CheckCoolTime()
    {
        return true;
    }
    override protected void DoSkill()
    {
        if (bulletIndex >= bullits.Length)
            bulletIndex = 0;
        
        bullits[bulletIndex++].GetComponent<Bullit>().StartFire(transform.position, bullitDistance, bullitDmg);
    }

    override protected void delay()
    {
        
    }

  

    IEnumerator SkillAction()
    {
        while (true)
        {
            Skill();
            yield return new WaitForSeconds(fireInterval);
        }
    }
}
