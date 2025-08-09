using TMPro;
using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    // ≈ÿΩ∫∆Æ
    [SerializeField]
    TextMeshProUGUI maxScore;
    [SerializeField]
    TextMeshProUGUI currentScore;

    private void Start()
    {
        maxScore.text = "Max Score : " + ((int)GameManager.Instance.GetMaxScore()).ToString();
        currentScore.text = "Score : " + ((int)GameManager.Instance.GetCurrentScore()).ToString();
    }
}
