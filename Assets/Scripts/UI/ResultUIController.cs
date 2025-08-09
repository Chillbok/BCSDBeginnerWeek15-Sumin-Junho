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
        maxScore.text = "Max Score : " + ((int)GameManager.instance.GetMaxScore()).ToString();
        currentScore.text = "Score : " + ((int)GameManager.instance.GetCurrentScore()).ToString();
    }
}
