using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SkipTrigger : MonoBehaviour
{
    public void SkipSynopsis() // 시놉시스 패스하기
    {
        SceneManager.LoadScene("stage1");
    }
}
