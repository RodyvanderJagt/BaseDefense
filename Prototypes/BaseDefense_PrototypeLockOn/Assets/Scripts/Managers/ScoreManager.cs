using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _baseMaxHealth = 1000;
    private int _baseHealth;
    private int _currentScore;  

    private void Start()
    {
        _currentScore = 0;
        _baseHealth = _baseMaxHealth;

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
