using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int _baseMaxHealth = 1000;
    private int _baseHealth;
    private int _currentScore = 0;

    public Events.OnScoreUpdate OnScoreUpdate;
    public Events.OnGameOver OnGameOver;

    private void Start()
    {
        GamePhaseManager.Instance.OnGamePhaseChanged += HandleGamePhaseChanged;

        //See if you can remove this later when starting game after level succesfully loaded.
        HandleGameStart();
    }

    #region Game phase

    private void HandleGamePhaseChanged(GamePhaseManager.GamePhase gamePhase)
    {
        switch (gamePhase)
        {
            case GamePhaseManager.GamePhase.STARTING:
                HandleGameStart();
                break;
            case GamePhaseManager.GamePhase.PLAYING:
                
                break;
            case GamePhaseManager.GamePhase.GAMEOVER:
                HandleGameOver();
                break;
            default:
                break;
        }
    }

    private void HandleGameStart()
    {
        _currentScore = 0;
        _baseHealth = _baseMaxHealth;

        EnemyUnit.OnUnitDied += UpdateScore;
        HandleOutOfBounds.OnDamageToBase += UpdateBaseHealth;
    }

    private void HandleGameOver()
    {
        EnemyUnit.OnUnitDied -= UpdateScore;
        HandleOutOfBounds.OnDamageToBase -= UpdateBaseHealth;
    }

    #endregion

    #region Score

    private void UpdateScore(int _scoreToAdd)
    {
        _currentScore += _scoreToAdd;
        OnScoreUpdate?.Invoke(_currentScore);
    }

    private void UpdateBaseHealth(int _healthLost)
    {
        _baseHealth -= _healthLost;
        if (_baseHealth <= 0)
        {
            OnGameOver?.Invoke();
        }
    }
    #endregion
}
