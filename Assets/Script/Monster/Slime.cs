using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonsterHaviour
{
    [Header("Tower전용 변수")]
    [Tooltip("총알을 담는 컨테이너")]
    [SerializeField] GameObject[] bullits;
    [Tooltip("총알 발사 간격")]
    [SerializeField] float fireInterval = 2;
    [Tooltip("총알의 데미지")]
    [SerializeField] int bullitDmg = 2;
    [Tooltip("bullits에 있는 불릿을 불러오는거기 때문에 Distance와 Speed를 잘 조정해야함")]
    [SerializeField] int bullitDistance;
    [Tooltip("bullits에 있는 불릿을 불러오는거기 때문에 Distance와 Speed를 잘 조정해야함")]
    [SerializeField] int bullitSpeed = 3;
    int bulletIndex = 0;
   
    private void Start()
    {
        StartCoroutine("SkillAction");
        LookForward();
    }

    override protected bool CheckCoolTime()
    {
        return true;
    }
    override protected void DoSkill()
    {
        if (bulletIndex >= bullits.Length)
            bulletIndex = 0;
        bullits[bulletIndex++].GetComponent<Bullit>().StartFire(this.gameObject);
        //bullits[bulletIndex++].GetComponent<Bullit>().StartFire(transform.position, bullitDistance, dirctoinV.x , bullitDmg);

    }

    override protected void delay()
    {
        
    }

    public int CallBullitSpeed()
    {
        return bullitSpeed;
    }
    public int CallBullitDmg() {
        return bullitDmg;
    }

    public int CallBullitDistance() {
        return bullitDistance;
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
