//게임의 점수를 계산하기 위한 스크립트
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    //현재 점수
    private float currentScore = 0f;
    private float maxScore;

    //amount만큼 점수 추가
    public void AddScore(float amount)
    {
        currentScore += amount;
    }

    public void ResetScore()
    {
        currentScore = 0f;
    }

    //현재 스코어 설정
    public void SetCurrentScore(float score)
    {
        currentScore = score;
    }

    //현재 스코어 반환
    public float GetCurrentScore()
    {
        return currentScore;
    }
}
