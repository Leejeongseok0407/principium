using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnoffGround : MonoBehaviour
{
    [SerializeField] float intervalTime = 2f;
    [SerializeField] GameObject ground;
    float time;


    private void Update()
    {
        if (ground.activeSelf == false)
        {
            time += Time.deltaTime;
            if (time > intervalTime)
                GroundOn();
        }
    }

    void GroundOn()
    {
        ground.SetActive(true);
        time = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("aa");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("dd");  
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("dd");
            StartCoroutine(OffGround());
        }
    }

    IEnumerator OffGround()
    {
        yield return new WaitForSeconds(intervalTime);
        ground.SetActive(false);
    }
}
