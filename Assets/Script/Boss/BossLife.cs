using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossLife : MonoBehaviour, IDamage
{
    [SerializeField]
    private Animator _patternAni;
    [SerializeField]
    private int _bossLife = 5;
    private bool _isCanAttack = false;
    public bool IsCanAttack
    {
        get { return _isCanAttack; }
        set { _isCanAttack = value; }
    }

    public void Die()
    {
        if (_bossLife > 0)
            return;
        _patternAni.Play("Die");
        Debug.Log("쥬금");
        //죽는 애니메이션 출력
        //죽은 뒤에 하는 뭐 클리어창.
    }

    public bool GetDamage()
    {
        if (_isCanAttack != true)
            return false;
        _bossLife--;
        _patternAni.Play("Hit");
        Debug.Log("Life 는" + _bossLife);
        _isCanAttack = false;
        Die();
        return true;
    }
}