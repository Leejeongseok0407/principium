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
        ani.SetBool("TrackingPlayer", true);
        ani.SetBool("MissingPlayer", false);
    }

    override protected void MissingPlayerAni()
    {
        ani.SetBool("TrackingPlayer", false);
        ani.SetBool("MissingPlayer", true);
    }

}
