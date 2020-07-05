using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterHaviour : MonoBehaviour
{
    [SerializeField] int type;
    [SerializeField] int hp;
    [SerializeField] int dmg;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // virtual로 오버라이딩 가능하게 만들어줌.
    // 이동 모션을 의미함
    protected virtual void Patten(int type)
    {

    }

    //굳이 있어야 하나 싶은데..
    protected virtual void Dead()
    {

    }

    //
    protected virtual void Attack()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("플레이어와 부딛힘");
        }
    }
}
