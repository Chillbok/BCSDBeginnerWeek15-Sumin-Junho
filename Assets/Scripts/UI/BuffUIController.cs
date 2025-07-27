using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIController : MonoBehaviour
{
    //참조 변수(버프 이름 및 지속시간 가져올 참조변수)
    [Header("참조 변수들")]
    public PlayerController thePlayerController;
    public GameObject buffUIPrefab; //버프가 생성될 TextMeshPro 프리팹 연결. 일단 Prefab > UI > BuffItemTemplate로 해둠.
    public Transform buffUIParent; //버프가 연결될 부모 Parent 연결

    //현재 활성화된 버프 UI들을 관리하는 딕셔너리
    [Header("딕셔너리")]
    private Dictionary<BuffType, GameObject> activeBuffUI = new Dictionary<BuffType, GameObject>();

    void Update()
    {
        if (thePlayerController == null) return; //참조 대상이 연결되었는지 확인

        UpdateBuffDisplay(); //버프 목록의 변경 사항 매 프레임마다 UI에 반영
    }

    void UpdateBuffDisplay() //버프를 UI에 추가할지 말지 판단하고 추가
    {
        //사라진 버프 UI 삭제
        //thePlayerController.buffRemainingTimes.Keys : 현재 활성화된 버프 타입 목록
        var buffsToRemove = activeBuffUI.Keys.Except(thePlayerController.buffRemainingTimes.Keys).ToList();
        foreach (var buffType in buffsToRemove)
        {
            if (activeBuffUI.ContainsKey(buffType) && activeBuffUI[buffType] != null)
            {
                Destroy(activeBuffUI[buffType]);
            }
            activeBuffUI.Remove(buffType);
        }

        //새 버프 UI 생성 또는 기존 UI 업데이트
        foreach (var buff in thePlayerController.buffRemainingTimes)
        {
            GameObject buffUIInstance;
            //이미 해당 버프의 UI가 있는지 확인
            if (activeBuffUI.ContainsKey(buff.Key))
            {
                buffUIInstance = activeBuffUI[buff.Key];
            }
            else //없다면 새로 생성
            {
                buffUIInstance = Instantiate(buffUIPrefab, buffUIParent);
                activeBuffUI.Add(buff.Key, buffUIInstance);
            }

            //텍스트 업데이트
            TextMeshProUGUI buffText = buffUIInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (buffText != null)
            {
                //buff.Key = BuffType, buff.Value = 남은 시간(float)
                float buffTimeMin = buff.Value / 60; //분
                float buffTimeSec = buff.Value % 60; //초
                buffText.text = $"{buff.Key} {(int)buffTimeMin}:{(int)buffTimeSec}";
            }
        }
    }
}
