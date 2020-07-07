using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;
    public void OnBtnClick() // 버튼 UI의 OnClick 이벤트에 연결
    {
        switch (currentType)
        {
            case BTNType.New:
                Debug.Log("처음부터");
                break;
            case BTNType.Continue:
                Debug.Log("불러오기");
                break;
        }
    }
}
