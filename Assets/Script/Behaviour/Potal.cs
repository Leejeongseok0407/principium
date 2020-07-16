using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] string scenename = "";
    [SerializeField] GameObject pressKey;

    private void Start()
    {
        pressKey.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                SceneManager.LoadScene(scenename);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            pressKey.SetActive(false);
    }
}
