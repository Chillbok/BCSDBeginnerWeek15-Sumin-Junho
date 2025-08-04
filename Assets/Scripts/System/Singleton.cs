using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    //외부에서 인스턴스에 접근하기 위한 프로퍼티
    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                //인스턴스가 아직 존재하지 않는 경우, 생성함
                if (_instance == null)
                {
                    Debug.LogWarning($"경고: {typeof(T)}가 인스턴스에서 발견되지 않고, 씬에서 찾지도 않음");
                }
                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        //인스턴스가 아직 설정되지않았다면, 이 객체를 인스턴스로 설정
        if (_instance == null)
        {
            _instance = this as T;
            //씬 전환 시 파괴되지 않도록 설정
            DontDestroyOnLoad(this.gameObject);
        }
        //인스턴스가 이미 존재하고, 이 객체가 그 인스턴스가 아니라면
        else if (_instance != this)
        {
            Debug.LogWarning($"{typeof(T)}와 동일한 한 개 이상의 인스턴스 발견! 삭제함!");
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
    }
}