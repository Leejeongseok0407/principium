using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonsterHaviour
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
        originAni.SetTrigger("DetectPlayer");
    }
    override protected void MissingPlayerAni()
    {
        originAni.SetTrigger("MissingPlayer");
    }
}
