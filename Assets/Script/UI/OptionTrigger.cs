using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionTrigger : MonoBehaviour
{
    public GameObject OptionCanvas;
    public GameObject SoundCanvas;
    public GameObject ShortcutCanvas;
    public GameObject TeamCanvas;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            OptionCanvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape)) // ESC 한번 더 누르면 옵션 창 나가기
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

    public void OpenSound() // 사운드 버튼 누르면 사운드 창 활성화
    {
        ShortcutCanvas.SetActive(false);
        TeamCanvas.SetActive(false);
        SoundCanvas.SetActive(true);
    }

    public void OpenShortCut() // 단축키 설명 버튼 누르면 단축키 버튼 활성화
    {
        SoundCanvas.SetActive(false);
        TeamCanvas.SetActive(false);
        ShortcutCanvas.SetActive(true);
    }

    public void OpenTeam()
    {
        SoundCanvas.SetActive(false);
        ShortcutCanvas.SetActive(false);
        TeamCanvas.SetActive(true);
    }

    public void LoadingStartScene() // 옵션창에서 시작화면으로 가는 버튼
    {
        SceneManager.LoadScene("start Scene");
    }

}

