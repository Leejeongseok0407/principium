using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] string scenename;
    [SerializeField] int getSkillNum = 0;
    [Tooltip("이미지를 표출시켜주고 싶으면 삽입 (없어도됨)")]
    [SerializeField] GameObject pressSprite;
    [SerializeField] Animator ani;

    private void Start()
    {
        pressKeyActive(false);
    }

    public void AddSkill() {
        PlayerPrefs.SetInt("SkillIndex", getSkillNum);
        Debug.Log("getSkill");
    }

    void pressKeyActive(bool set) {
        if (pressSprite != null)
            pressSprite.SetActive(set);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetBool("PlayerEnter",true);
            pressKeyActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (PlayerPrefs.GetInt("SkillIndex") == getSkillNum - 1)
                    AddSkill();
                SceneManager.LoadScene(scenename);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ani.SetBool("PlayerEnter", false);
            pressKeyActive(false);
        }
    }

}
