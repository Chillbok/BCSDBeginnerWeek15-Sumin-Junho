using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [Header("설정 슬라이더")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider mouseSensSlider;

    [Header("음성 설정을 위한 오브젝트들")]
    [SerializeField] GameObject sfxManager;
    [SerializeField] GameObject bgmManager;

    [Header("컴포넌트 참조 변수들")]
    [SerializeField] PlayerController playerController;
    [SerializeField] GameManager gameManager;
    //SettingObjects settingObjects;

    string sceneName; //씬 이름 저장하기 위한 변수

    float bgmVolume;
    float sfxVolume;
    float mouseSens;

    protected override void Awake()
    {
    }

    void Start()
    {
        ApplySetting(); //설정값 불러와서 설정 적용
    }

    private void Update()
    {
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
        SetSceneName(scene, sceneName);

        //플레이어 컨트롤러가 인스펙터에서 비어있는 경우
        if (playerController == null)
        {
            FindPlayerController();
            //FindSettings();
        }
    }

    //플레이어가 없는 경우, 플레이어를 찾아 배정하는 메서드
    void FindPlayerController()
    {
        if (sceneName != gameManager.nameOfPlayScene)
        {
            Debug.Log("Player가 존재하지 않습니다");
        }
        else
        {
            //플레이어 찾기
            GameObject playerObject = GameObject.FindWithTag("Player");
            playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    //세팅 오브젝트 찾기
    /*
    void FindSettings()
    {
        if (sceneName == gameManager.nameOfStartScene || sceneName == gameManager.nameOfPlayScene)
        {
            //설정 게임 오브젝트 찾기
            GameObject settingsGameObject = GameObject.FindWithTag("Setting Objects");
            if (settingsGameObject == null) Debug.LogError($"{settingsGameObject.name} 존재하지 않음!");
            settingObjects = settingsGameObject.GetComponent<SettingObjects>();

            //슬라이더 배정
            sfxSlider = settingObjects.sfxSlider;
            bgmSlider = settingObjects.bgmSlider;
            mouseSensSlider = settingObjects.mouseSensitivity;
        }

        else return;
    }
    */

    //지정한 변수에 씬의 이름을 넣는 메서드
    string SetSceneName(Scene scene, string sceneName)
    {
        sceneName = scene.name;
        return sceneName;
    }

    //슬라이더의 설정 반영하는 메서드
    //설정이 바뀐 경우에만 수정 실행
    /*
    void ChangeSetting()
    {
        if (DefineSettingChange(sfxVolume, sfxSlider.value))
            sfxVolume = sfxSlider.value;
        if (DefineSettingChange(bgmVolume, bgmSlider.value))
            bgmVolume = bgmSlider.value;
        if (DefineSettingChange(mouseSens, mouseSensSlider.value))
            mouseSens = mouseSensSlider.value;
    }
    */

    //설정이 바뀌었다면 true 반환
    bool DefineSettingChange(float saved, float current)
    {
        if (saved != current) return true;
        else return false;
    }

    //설정한 내용을 게임 데이터에 저장하는 메서드
    public void SaveSetting()
    {
        Debug.Log("설정 저장 완료");
        GameData gameData = GameManager.Instance.gameData;
        gameData.sfxVolume = sfxVolume;
        gameData.bgmVolume = bgmVolume;
        gameData.mouseSensitivity = mouseSens;
    }

    //설정 적용하는 메서드
    void ApplySetting()
    {
        GameData gameData = GameManager.Instance.gameData;
        sfxVolume = gameData.sfxVolume;
        bgmVolume = gameData.bgmVolume;
        mouseSens = gameData.mouseSensitivity;
    }
}