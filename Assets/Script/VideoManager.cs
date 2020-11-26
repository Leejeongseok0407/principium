using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public float time = 0;    

    public void Update()
    {
        time += Time.deltaTime;
        if (time > 18)
            SceneManager.LoadScene("start Scene");

    }

}
