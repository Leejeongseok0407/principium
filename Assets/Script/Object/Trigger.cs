using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject pressSprite;
    [SerializeField] GameObject wall;
    bool coolTime;
    bool inPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        pressKeyActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(inPlayer); if (inPlayer == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Set");
            WallKeyActive(!wall.activeSelf);
        }
    }

    void pressKeyActive(bool set)
    {
        if (pressSprite != null)
            pressSprite.SetActive(set);
    }

    void WallKeyActive(bool set)
    {
        if (coolTime == true)
            return;
        if (wall != null)
        {
            wall.SetActive(set);
            StartCoroutine("CoolTime");
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressKeyActive(true);
            inPlayer = true;
        }
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("in");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PlayerIn");
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Set");
                WallKeyActive(!wall.activeSelf);
            }
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressKeyActive(false);
            inPlayer = false;
        }
    }

    IEnumerator CoolTime()
    {
        coolTime = true;
        yield return new WaitForSeconds(1f);
        coolTime = false;
    }
}
