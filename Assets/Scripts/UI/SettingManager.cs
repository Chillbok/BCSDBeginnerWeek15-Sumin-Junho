using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [Header("음성 설정을 위한 오브젝트들")]
    [SerializeField] GameObject sfxManager;
    [SerializeField] GameObject bgmManager;

    [Header("컴포넌트 참조 변수들")]
    [SerializeField] PlayerController playerController;
    [SerializeField] GameManager gameManager;
    //SettingObjects settingObjects;

    string sceneName; //씬 이름 저장하기 위한 변수

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //씬이 처음 로드되면 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"SettingManager: {scene.name} 씬이 로드되었습니다.");
        
        //씬의 이름을 변수에 배정 
        SetSceneName(scene);

        //플레이어 컨트롤러가 인스펙터에서 비어있는 경우
        if (gameManager != null && sceneName == gameManager.nameOfPlayScene && playerController == null)
        {
            FindPlayerController();
        }
    }

    //플레이어가 없는 경우, 플레이어를 찾아 배정하는 메서드
    void FindPlayerController()
    {
        if (gameManager != null && sceneName != gameManager.nameOfPlayScene)
        {
            Debug.Log("Player가 존재하지 않습니다");
        }
        else
        {
            //플레이어 찾기
            GameObject playerObject = GameObject.FindWithTag("Player");

            if (playerObject != null)
                playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    //지정한 변수에 씬의 이름을 넣는 메서드
    void SetSceneName(Scene scene)
    {
        sceneName = scene.name;
    }
}