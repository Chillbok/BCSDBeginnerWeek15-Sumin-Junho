//설정의 슬라이더에서 사용할 스크립트
//구현 목표: 스크립트를 건드려도, 입력값에 입력해도 설정을 적용할 수 있어야 함
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlideController : MonoBehaviour
{
    [Header("UI 요소들")]
    public Slider slider;
    public TMP_InputField inputField;

    void Start()
    {
        //초기값 설정
        //퍼센티지 기준이라면
        if (slider.maxValue == 100) inputField.text = "100";
        //퍼센티지 기준이 아니라면
        else inputField.text = "1";

        //각 UI 요소에 이벤트 리스너 추가
        //슬라이더 값이 변경될 때마다 OnSliderValueChanged 메서드 호출
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        //인풋 필드의 입력이 완료되면 OnInputFieldValueChanged 메서드 호출
        inputField.onEndEdit.AddListener(OnInputFieldValueChanged);
    }

    //슬라이더를 조작했을 때 출력될 메서드
    private void OnSliderValueChanged(float value)
    {
        //슬라이더값을 정수로 변환해 인풋 필드의 텍스트 업데이트
        inputField.text = Mathf.RoundToInt(value).ToString();

        //실제로 값을 조작하는 과정을 아래에 추가
    }

    private void OnInputFieldValueChanged(string value)
    {
        //입력한 텍스트를 float로 변환
        if (float.TryParse(value, out float floatValue))
        {
            //변환된 값을 슬라이더의 min/max 값으로 제한
            floatValue = Mathf.Clamp(floatValue, slider.minValue, slider.maxValue);

            //슬라이더 값 업데이트
            slider.value = floatValue;

            //입력한 값도 슬라이더의 제한값 안으로 변경(200 입력하면 100으로)
            inputField.text = Mathf.RoundToInt(floatValue).ToString();
        }
        else //숫자로 입력 실패하면
        {
            Debug.Log("숫자로 변환 실패함!");
            inputField.text = Mathf.RoundToInt(slider.value).ToString();
        }
    }
}
