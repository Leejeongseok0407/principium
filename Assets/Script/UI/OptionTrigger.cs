using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionTrigger : MonoBehaviour
{
    public GameObject optionCanvas;

    public void OptionDown()
    {
        Time.timeScale = 0;
        optionCanvas.SetActive(true);
    }
}
