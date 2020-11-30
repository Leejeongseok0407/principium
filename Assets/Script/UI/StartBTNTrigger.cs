using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBTNTrigger : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionDetail;

    void Update()
    {
        if (OptionDetail.activeSelf == false) //Option 창 활성화 안 돼있으면 메인메뉴 창 활성화
            MainMenu.SetActive(true);
        else if(OptionDetail.activeSelf == true)
            MainMenu.SetActive(false);
    }
    public void StartGame() // 시작화면에서 게임 처음부터 시작하는 버튼
    {
        SceneManager.LoadScene("prologue");
    }
    public void ExitGame() // 시작화면에서 게임 종료하기
    {
        Application.Quit();
    }
}
