using Unity.VisualScripting;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    //HP 관련 변수
    [SerializeField]
    GameObject HpBar; //HP 바
    [SerializeField]
    GameObject HpText; //HP 텍스트

    //SP 관련 변수
    [SerializeField]
    GameObject SpBar; //스태미나 바

    //참조 변수
    [SerializeField]
    PlayerController thePlayerController; //PlayerController.cs

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
