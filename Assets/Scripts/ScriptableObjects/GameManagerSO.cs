using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerScriptableObject", menuName = "Scriptable Objects/GameManagerScriptableObject", order = int.MaxValue)]
public class GameManagerSO : ScriptableObject
{
    [Header("게임 주요 씬들")]
    [Tooltip("타이틀 화면 씬 이름")]
    [SerializeField]
    private string _nameOfTitleScene;
    public string NameOfTitleScene { get { return _nameOfTitleScene; } }

    [Tooltip("타이틀 화면 씬 이름")]
    [SerializeField]
    private string _nameOfPlayScene;
    public string NameOfPlayScene { get { return _nameOfPlayScene; } }

    [Tooltip("타이틀 화면 씬 이름")]
    [SerializeField]
    private string _nameOfResultScene;
    public string NameOfResultScene { get { return _nameOfTitleScene; } }

}