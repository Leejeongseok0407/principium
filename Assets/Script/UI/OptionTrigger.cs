using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionTrigger : MonoBehaviour
{
    public GameObject OptionDetail;
    public GameObject SoundDetail;
    public GameObject ShortcutDetail;
    public GameObject TeamDetail;

    public void Update()
    {
        GetKeyDownOption();
    }

  

    void GetKeyDownOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffOption();
        }
    }

    public void OnOffOption() {
        if (OptionDetail.activeSelf == false) // ESC 한번 더 누르면 옵션 창 나가기
        {
            Time.timeScale = 0;
            OptionDetail.SetActive(true);
        }
        else if(OptionDetail.activeSelf == true) // ESC 한번 더 누르면 옵션 창 나가기
        {
            OptionDetail.SetActive(false);
            Time.timeScale = 1;
        }
    }

    
    public void OptionDown() // 옵션 버튼 누르면 옵션 창 활성화
    {
        Time.timeScale = 0;
        OptionDetail.SetActive(true);
    }

    public void OpenSound() // 사운드 버튼 누르면 사운드 창 활성화
    {
        ShortcutDetail.SetActive(false);
        TeamDetail.SetActive(false);
        SoundDetail.SetActive(true);
    }

    public void OpenShortCut() // 단축키 설명 버튼 누르면 단축키 버튼 활성화
    {
        SoundDetail.SetActive(false);
        TeamDetail.SetActive(false);
        ShortcutDetail.SetActive(true);
    }

    public void OpenTeam()
    {
        SoundDetail.SetActive(false);
        ShortcutDetail.SetActive(false);
        TeamDetail.SetActive(true);
    }

    public void LoadingStartScene() // 옵션창에서 시작화면으로 가는 버튼
    {
        SceneManager.LoadScene("start Scene");
    }

}

