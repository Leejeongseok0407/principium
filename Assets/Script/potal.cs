using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] string scenename;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            SceneManager.LoadScene(scenename);
        }
    }
}
