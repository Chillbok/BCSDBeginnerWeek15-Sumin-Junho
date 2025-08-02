//버튼의 기능을 구현한 스크립트
//모든 버튼의 기능을 이곳에 서술함
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //게임 종료시키는 스크립트
    public void QuitGame()
    {
#if UNITY_EDITOR //유니티 에디터라면
        UnityEditor.EditorApplication.isPlaying = false;
#else //유니티 에디터가 아니라면
        Application.Quit();
#endif
    }

    //특정 씬으로 이동하는 스크립트
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
