using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _currentScore;

    [SerializeField] private int _baseHealth = 1000;

    private void Start()
    {
        _currentScore = 0;

        EnemyUnit.OnUnitDied += UpdateScore;
        HandleOutOfBounds.OnUnitInBase += UpdateBaseHealth;
    }

    private void UpdateScore(int _scoreToAdd)
    {
        _currentScore += _scoreToAdd;
    }

    private void UpdateBaseHealth(int _healthLost)
    {
        _baseHealth -= _healthLost;
    }

}
