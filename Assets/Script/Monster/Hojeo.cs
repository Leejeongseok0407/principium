using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hojeo : MonsterHaviour
{

    override protected bool CheckCoolTime()
    {
        return true;
    }

    override protected void delay()
    {

    }


    override protected void DoSkill()
    {

    }

    override protected void DetectPlayerAni()
    {
        ani.SetTrigger("TarckingPlayer");
    }

    override protected void MissingPlayerAni()
    {
        ani.SetTrigger("MissingPlyer");
    }

}
