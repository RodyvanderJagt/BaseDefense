using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _highscoreButton;
    [SerializeField] Button _exitButton;

    public event Action OnSettingsPressed;
    public event Action OnHighscorePressed;

    private void Start()
    {
        _startButton.onClick.AddListener(HandleStartClicked);
        _settingsButton.onClick.AddListener(HandleSettingsClicked);
        _highscoreButton.onClick.AddListener(HandleHighscoreClicked);
        _exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void HandleStartClicked()
    {
        GameManager.Instance.StartGame();
    }

    private void HandleSettingsClicked()
    {
        OnSettingsPressed?.Invoke();
    }

    private void HandleHighscoreClicked()
    {
        OnHighscorePressed?.Invoke();
    }

    private void HandleExitClicked()
    {
        Application.Quit();
    }

}
