using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionTrigger : MonoBehaviour
{
    public GameObject OptionCanvas;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0;
            OptionCanvas.SetActive(true);

            if (Input.GetButtonDown("Cancel")) // ESC 한번 더 누르면 옵션 창 나가기
            {
                OptionCanvas.SetActive(false);
                Time.timeScale = 1;
            }
                
        }
            
    }
    public void OptionDown() // 옵션 버튼 누르면 옵션 창 활성화
    {
        Time.timeScale = 0;
        OptionCanvas.SetActive(true);
    }
    public void LoadingStartScene() // 옵션창에서 시작화면으로 가는 버튼
    {
        SceneManager.LoadScene("start Scene");
    }

}

