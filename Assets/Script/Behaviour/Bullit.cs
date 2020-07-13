using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 dirctoinV;
    Vector2 distanceV;
    [SerializeField] int direction = 1;
    [SerializeField] int speed= 1;
    [SerializeField] GameObject bullitContainer;
    bool isFire= false;
    public int dmg = 0;

    private void Start()
    {
        transform.position = bullitContainer.transform.position;
    }

    private void Update()
    {
        if (isFire == true)
            Fire();
    }

    public void StartFire(Vector3 towerPosition, int bullitDistance, int bullitdmg) {
        transform.position = towerPosition;
        Debug.Log("Go! Fire");
        distanceV.x = towerPosition.x + bullitDistance;
        isFire = true;
        dmg = bullitdmg;
    }

    public void Fire()
    {
        dirctoinV = new Vector2(direction, 0);
        transform.Translate(dirctoinV * speed * Time.smoothDeltaTime, Space.World);
        if (distanceV.x < transform.position.x)
        {
            transform.position = bullitContainer.transform.position;
            isFire = false;
            Debug.Log("Stop Fire!!");
        }
        //방향 바꿔주는 함수
    }
}
