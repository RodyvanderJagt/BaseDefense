
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _exitButton;

    public event Action OnSettingsButtonClicked;

    private void Start()
    {
        _resumeButton.onClick.AddListener(HandleResumeButtonClicked);
        _settingsButton.onClick.AddListener(HandleSettingsButtonClicked);
        _restartButton.onClick.AddListener(HandleRestartButtonClicked);
        _exitButton.onClick.AddListener(HandleExitButtonClicked);
    }

    private void HandleResumeButtonClicked()
    {
        GameManager.Instance.TogglePause();
    }

    private void HandleSettingsButtonClicked()
    {
        OnSettingsButtonClicked?.Invoke();
    }

    private void HandleRestartButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }
    private void HandleExitButtonClicked()
    {
        GameManager.Instance.ExitGame();
    }

}
