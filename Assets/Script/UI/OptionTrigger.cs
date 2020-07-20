using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionTrigger : MonoBehaviour
{
    public GameObject OptionCanvas;

    public void OptionDown()
    {
        Time.timeScale = 0;
        OptionCanvas.SetActive(true);
    }
}
