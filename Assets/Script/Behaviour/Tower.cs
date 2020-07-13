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

    override protected void Skill() {
        if (bulletIndex < bullits.Length)
            bullits[bulletIndex++].GetComponent<Bullit>().StartFire(transform.position, bullitDistance, bullitDmg);
        else
        {
            bulletIndex = 0;
            bullits[bulletIndex++].GetComponent<Bullit>().StartFire(transform.position, bullitDistance, bullitDmg);
        }
    }
    IEnumerator SkillAction()
    {
        yield return new WaitForSeconds(fireInterval);
        Skill();
        StartCoroutine("SkillAction");
    }
}
