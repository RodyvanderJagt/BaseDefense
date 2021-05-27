using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhaseManager : Singleton<GamePhaseManager>
{
    public enum GamePhase
    {
        STARTING,
        PLAYING,
        GAMEOVER
    }
    public GamePhase CurrentGamePhase
    {
        get { return _currentGamePhase; }
        private set { _currentGamePhase = value; }
    }

    private GamePhase _currentGamePhase = GamePhase.STARTING;

    public Events.OnGamePhaseChanged OnGamePhaseChanged;

    [SerializeField] int _countdownSeconds = 3;

    private void Start()
    {
        ScoreManager.Instance.OnGameOver += HandleGameOver;

        HandleGameStart();
    }
    private void HandleGameStart()
    {
        UpdateGamePhase(GamePhase.STARTING);
        StartCoroutine(nameof(CountDownSequence), _countdownSeconds);
    }

    private void HandleGameOver()
    {
        UpdateGamePhase(GamePhase.GAMEOVER);
    }

    private void UpdateGamePhase(GamePhase phase)
    {
        _currentGamePhase = phase;
        OnGamePhaseChanged?.Invoke(phase);
    }

    IEnumerator CountDownSequence(int seconds)
    {
        while (seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;
        }
        UpdateGamePhase(GamePhase.PLAYING);
    }
}
