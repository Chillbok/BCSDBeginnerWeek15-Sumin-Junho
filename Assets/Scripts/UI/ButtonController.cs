//버튼의 기능을 구현한 스크립트
//모든 버튼의 기능을 이곳에 서술함
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //게임 종료시키는 스크립트
    public void QuitGame()
    {
        Application.Quit();
    }

    //특정 씬으로 이동하는 메서드
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //특정 게임오브젝트를 활성화하는 메서드
    public void ActivateGameObject(GameObject obj)
    {
        //게임 오브젝트 활성화하는 코드
        if (!obj.activeSelf)
            obj.SetActive(true);
        return;
    }

    //특정 게임오브젝트를 비활성화하는 메서드
    public void DeactiveGameObject(GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive(false);
        return;
    }

    //특정 게임 오브젝트의 활성화/비활성화 상태를 전환하는 메서드
    public void ToggleGameObjectActive(GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive(false);
        else
            obj.SetActive(true);
    }

    // 버튼 효과음 재생
    public void ClickSound()
    {
        SoundManager.Instance.PlaySFX("click");
    }
}
