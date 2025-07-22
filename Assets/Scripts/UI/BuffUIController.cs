using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIController : MonoBehaviour
{

    //참조 변수(버프 이름 및 지속시간 가져올 참조변수)

    //가져올 변수
    private string buffName; //버프 이름
    private float buffDuration; //버프 지속시간

    void Start()
    {
    }

    void Update()
    {
    }

    //입력한 버프 시간, 버프 이름을 반환 --> 버프이름 버프시간(분):버프시간(초)
    public string writeBuffText(string buffName, float buffDuration)
    {
        int minutes = Mathf.FloorToInt(buffDuration / 60); //버프 시간 분
        int seconds = Mathf.FloorToInt(buffDuration % 60); //버프 시간 초

        if (minutes != 0 && seconds != 0) //만약 지속시간이 아직 분, 초 모두 아직 0이 아닌 경우
        {
            string buffStatus = $"{buffName} {minutes}:{seconds}";
            return buffStatus;
        }
        else //지속시간이 분, 초 모두 0인 경우
            return ""; //빈칸 반환
    }
}
