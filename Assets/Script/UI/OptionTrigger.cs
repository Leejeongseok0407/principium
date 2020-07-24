using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionTrigger : MonoBehaviour
{
    public GameObject OptionCanvas;
    public GameObject SoundCanvas;
    public GameObject ShortcutCanvas;
    public GameObject TeamCanvas;

    public void Update()
    {
        GetKeyDownOption();
    }

    //일단 첫번쨰 길면 함수로 뺴욤
    //FixedUpdate는 한 프레임당 호출 1번, Update는 그냥 겁나 호출
    //세번째 함수가 혜진씨가 생각한 대로 호출되는게 아니라 두번 연속으로 호출해서 키고 바로 꺼진거에요
    //혜진씨의도는 알겠는데 컴퓨터는 라인으로 따라간다고 생각하면 이해하기 편해요 보
    //EscapeCode를 한번 눌렀으니까 true값으로 아래 전채가 실행되는건데 실행도중이니까 값이 true겠죠? 그래서 아래꺼도 같이 한번에 실행되는거에요
    //결국 켜졌다가 바로 꺼졌겠죠?
    //이러는 경우 Debug.log("쓰고싶은말");을 통해서 콘솔창에 이 표시가 나오는지 확인해봐요 제가 나중에 알켜dream 동방오셈
    //원래 함수는 아래썼으니까 뭐가 바뀐건지 비교해봐요
    //여기 밑에 보면 activeSelf라는 거 보이죠? 저도 몰라서 찾아봤는데 이런 기능이 있더라고요
    //그 OptionCanvas의 Active값을 반환해주는건데 이걸 통해서 제어하면 될거같아요
    //읽으면 주석 삭제 ㄱㄱ

    void GetKeyDownOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffOption();
        }
    }

    public void OnOffOption() {
        if (OptionCanvas.activeSelf == false) // ESC 한번 더 누르면 옵션 창 나가기
        {
            Time.timeScale = 0;
            OptionCanvas.SetActive(true);
        }
        else if(OptionCanvas.activeSelf == true) // ESC 한번 더 누르면 옵션 창 나가기
        {
            OptionCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    /*
        void OnOffOption() {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                OptionCanvas.SetActive(true);
     //           Debug.log("실행중"); 제가 추가한 붑분
                if (Input.GetKeyDown(KeyCode.Escape)) // ESC 한번 더 누르면 옵션 창 나가기
                {
                    OptionCanvas.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }*/
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

