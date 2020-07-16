using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 dirctoinV;
    Vector2 distanceV;
    float direction = 1;
    int speed = 1;
    [SerializeField] GameObject bullitContainer;
    bool isFire= false;
    int dmg = 0;

    private void Start()
    {
        transform.position = bullitContainer.transform.position;
    }

    private void Update()
    {
        if (isFire == true)
            Fire();
    }

    public void StartFire(GameObject tower) {
        transform.position = tower.transform.position;
        dirctoinV.x = tower.GetComponent<Tower>().ReturnDirctoinV().x;
        distanceV.x = tower.transform.position.x + tower.GetComponent<Tower>().CallBullitDistance() * dirctoinV.x;
        dmg = tower.GetComponent<Tower>().CallBullitDmg();
        speed = tower.GetComponent<Tower>().CallBullitSpeed();
        isFire = true;
    }

    void LookForward()
    {
        if (dirctoinV.x > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        if (dirctoinV.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    public void Fire()
    {
        LookForward();
        transform.Translate(dirctoinV * speed * Time.smoothDeltaTime, Space.World);
        if (Mathf.Abs(distanceV.x) < Mathf.Abs(transform.position.x))
        {
            transform.position = bullitContainer.transform.position;
            isFire = false;

        }
    }

    public int CallBullitDmg()
    {
        return dmg;
    }
    public float CallDirctionX()
    {
        return dirctoinV.x;
    }
}
