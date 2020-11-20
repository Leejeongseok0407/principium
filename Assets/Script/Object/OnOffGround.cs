using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffGround : MonoBehaviour
{
    [SerializeField] float intervalTime = 2f;
    [SerializeField] float StartTime = 1f;
    [SerializeField] GameObject ground;

    float time;


    private void Update()
    {
        time += Time.deltaTime;
        if (time > StartTime + intervalTime)
        GroundOnAndOff();
    }

    void GroundOnAndOff() {
        if (ground.activeSelf == false)
            ground.SetActive(true);
        else 
            ground.SetActive(false);
        time = StartTime;
    }

}
