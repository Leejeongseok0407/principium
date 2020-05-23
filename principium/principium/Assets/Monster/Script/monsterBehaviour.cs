using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract 추상클래스임을 명시
public class monsterHaviour : MonoBehaviour
{
    [SerializeField] int type;
    [SerializeField] int hp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // virtual로 오버라이딩 가능하게 만들어줌.
    protected virtual void Patten(int type)
    {

    }

    protected virtual void Dead()
    {

    }

    protected virtual void Attack()
    {

    }
}
