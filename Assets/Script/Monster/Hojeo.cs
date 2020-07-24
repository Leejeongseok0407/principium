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

    override protected void TarckingPlayerAni()
    {
        originAni.SetTrigger("TarckingPlayer");
    }

    override protected void MissingPlayerAni()
    {
        originAni.SetTrigger("MissingPlyer");
    }

}
