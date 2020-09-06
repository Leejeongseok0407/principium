using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBTNTrigger : MonoBehaviour
{
    public GameObject MainMenu;

    public void StartGame() // 시작화면에서 게임 처음부터 시작하는 버튼
    {
        SceneManager.LoadScene("prologue");
    }
    public void ExitGame() // 시작화면에서 게임 종료하기
    {
        Application.Quit();
    }
}
