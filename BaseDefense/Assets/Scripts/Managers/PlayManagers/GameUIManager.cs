using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUIManager : MonoBehaviour
{
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI _scoreText;

    [Header("Screens")]
    [SerializeField] GameObject _titleScreen;
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _scoreScreen;
    List<GameObject> _screenList = new List<GameObject>();


    private void Start()
    {
        ScoreManager.Instance.OnScoreUpdate += UpdateScore;
        GamePhaseManager.Instance.OnGamePhaseChanged += HandleGamePhaseChanged;

        _screenList.Add(_titleScreen);
        _screenList.Add(_gameOverScreen);
        _screenList.Add(_scoreScreen);

        
    }

    private void SetActiveScreen(GameObject activeScreen)
    {
        foreach (GameObject screen in _screenList)
        {
            if (screen != null)
            {
                screen.gameObject.SetActive(false);
            }
        }
        if (activeScreen != null)
        {
            activeScreen.gameObject.SetActive(true);
        }
    }

    private void HandleGamePhaseChanged(GamePhaseManager.GamePhase gamePhase)
    {
        switch (gamePhase)
        {
            case GamePhaseManager.GamePhase.STARTING:
                SetActiveScreen(_titleScreen);
                break;
            case GamePhaseManager.GamePhase.PLAYING:
                SetActiveScreen(_scoreScreen);
                break;
            case GamePhaseManager.GamePhase.GAMEOVER:
                SetActiveScreen(_gameOverScreen);
                break;
            default:
                SetActiveScreen(null);
                break;
        }
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }



}
