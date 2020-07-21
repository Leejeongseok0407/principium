using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    public void OnBtnClick() // 버튼 UI의 OnClick 이벤트에 연결
    {
        switch (currentType)
        {
            case BTNType.Start:
                SceneManager.LoadScene("stage1");
                break;
            case BTNType.Load:
                Debug.Log("불러오기");
                break;
            case BTNType.Option:
                Debug.Log("옵션");
                break;
            case BTNType.Sound:
                Debug.Log("소리");
                break;
            case BTNType.ShortCut:
                Debug.Log("단축키");
                break;
            case BTNType.Team:
                Debug.Log("개발자");
                break;
            case BTNType.Back:
                Debug.Log("뒤로가기");
                break;
            case BTNType.Quit:
                Application.Quit();
                break;
        }
    }
}
