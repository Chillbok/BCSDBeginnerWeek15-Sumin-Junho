//게임의 일시정지 메뉴의 작동을 관리하기 위한 스크립트
//게임매니저에 붙여 사용한다.
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StopMenuController : MonoBehaviour
{
    [Header("참조 변수")]
    [SerializeField]
    PlayerController playerController;

    [Header("게임 오브젝트들")]
    [SerializeField]
    GameObject stopMenu; //일시정지 메뉴
    [SerializeField]
    GameObject[] menus; //일시중지 화면에서 추가로 사용할 설정 메뉴들 모음

    //게임 상태변수

    void Start()
    {
        stopMenu.SetActive(false); //게임메뉴 비활성화
    }

    void Update()
    {
        bool isPaused = GameManager.Instance.isPaused;
        //ESC 누르면 게임 일시정지 후 메뉴 출력
        //if (Input.GetKeyDown(KeyCode.Escape))
        if (Input.GetKeyDown(KeyCode.Alpha1)) //이후에 위쪽 주석처리된 if문으로 수정 필요
        {
            if (isPaused)
            {
                ResumeTime(); //게임 다시 이어하기
                stopMenu.SetActive(false);
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
            }
            else
            {
                PauseTime(); //게임 시간 멈춤
                stopMenu.SetActive(true);
                DeactivePauseMenus();
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
            }
        }

        //일시정지 상태인데 메뉴가 비활성화되면 게임 재개
        if (GameManager.Instance.isPaused && !stopMenu.activeSelf)
        {
            ResumeTime();
            //커서 지우기
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }

    //메뉴 열린 후 모든 메뉴 숨기기
    private void DeactivePauseMenus()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }

    //게임시간 멈추는 메서드
    public void PauseTime()
    {
        Time.timeScale = 0f;
        GameManager.Instance.isPaused = true;
        Debug.Log("게임 시간 멈춤");
    }

    //게임 시간 다시 흘러가게 하는 메서드
    public void ResumeTime()
    {
        Time.timeScale = 1f;
        GameManager.Instance.isPaused = false;
        Debug.Log("게임 시간 다시 흘러감");
    }
}
