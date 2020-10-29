using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject pressSprite;
    [SerializeField] GameObject wall;
    [SerializeField] Animator ani = null;
    [SerializeField] ParticleSystem particle;
    bool coolTime = false;
    bool inPlayer =false;

    // Start is called before the first frame update
    void Start()
    {
        PressImageActive(false);
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        PressImageActive(inPlayer);

        if (inPlayer == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            WallActive(!wall.activeSelf);
        }
    }

    void PressImageActive(bool set)
    {
        if (pressSprite != null)
        {
            if (coolTime == true)
            {
                pressSprite.SetActive(false);
                return;
            }
            pressSprite.SetActive(set);
        }
    }

    void WallActive(bool wallOn)
    {
        if (coolTime == true)
            return;
        if (wall != null)
        {
            if (wallOn == false)
                particle.Play();
            ani.SetBool("On", !wallOn);
            wall.SetActive(wallOn);
            StartCoroutine("CoolTime");
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
