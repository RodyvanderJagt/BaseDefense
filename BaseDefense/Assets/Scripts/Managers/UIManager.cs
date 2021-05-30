using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Menus")]
    [SerializeField] MainMenu _mainMenu;
    [SerializeField] SettingsMenu _settingsMenu;
    [SerializeField] HighscoreMenu _highscoreMenu;
    [SerializeField] PauseMenu _pauseMenu;
    List<Menu> _menuList = new List<Menu>();
    [SerializeField] Camera _dummyCamera;
    Image _menuBackground;
   

    //Current UI settings
    float _currentFontSize;
    string _currentPlayerName;

    public string PlayerName => _currentPlayerName;
    public float CurrentFontSize => _currentFontSize;

    public event Action<float> OnFontSizeChangedRatio;
    public event Action<float> OnAudioVolumeChanged;

    private void Start()
    {
        //Default settings initialized
        _currentPlayerName = "Player";
        _currentFontSize = 1.0f;

        _menuList.Add(_mainMenu);
        _menuList.Add(_settingsMenu);
        _menuList.Add(_pauseMenu);
        _menuList.Add(_highscoreMenu);

        LoadAllMenus();
        SetActiveMenu(_mainMenu);

        _menuBackground = GetComponent<Image>();

        //Main Menu listeners
        _mainMenu.OnSettingsPressed += HandleSettingsPressed;
        _mainMenu.OnHighscorePressed += HandleHighscorePressed;

        //SettingsMenu listeners
        _settingsMenu.OnBackButtonClicked += HandleBackButtonPressed;
        _settingsMenu.OnPlayerNameChanged += HandlePlayerNameChanged;
        _settingsMenu.OnFontSizeChanged += HandleFontSizeChanged;
        _settingsMenu.OnAudioVolumeChanged += HandleAudioVolumeChanged;

        //HighscoreMenu listeners
        _highscoreMenu.OnBackButtonPressed += HandleBackButtonPressed;

        //Game state change
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void LoadAllMenus()
    {

        foreach (Menu menu in _menuList)
        {
            if (menu != null)
            {
                menu.gameObject.SetActive(true);
                menu.gameObject.SetActive(false);
            }
        }
    }


    private void SetActiveMenu(Menu activeMenu)
    {
        foreach (Menu menu in _menuList)
        {
            if (menu != null)
            {
                menu.gameObject.SetActive(false);
            }
        }
        if (activeMenu != null)
        {
            activeMenu.gameObject.SetActive(true);
        }
    }

    void HandleGameStateChanged(GameManager.GameState newState, GameManager.GameState previousState)
    {
        if (newState == GameManager.GameState.RUNNING)
        {
            SetActiveMenu(null);
            _menuBackground.enabled = false;
            _dummyCamera.enabled = false;
        }
        if (newState == GameManager.GameState.PREGAME)
        {
            SetActiveMenu(_mainMenu);
            _menuBackground.enabled = true;
            _dummyCamera.enabled = true;
        }
        if (newState == GameManager.GameState.PAUSED)
        {
            SetActiveMenu(_pauseMenu);
        }
    }

    #region mainMenu handlers

    private void HandleSettingsPressed()
    {
        SetActiveMenu(_settingsMenu);
    }

    private void HandleHighscorePressed()
    {
        SetActiveMenu(_highscoreMenu);
    }

    #endregion

    #region settingsMenu handlers

    private void HandleBackButtonPressed()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME)
        {
            SetActiveMenu(_mainMenu);
        }
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
        {
            SetActiveMenu(_pauseMenu);
        }
    }
    private void HandlePlayerNameChanged(string playerName)
    {
        _currentPlayerName = playerName;
    }

    private void HandleFontSizeChanged(float newFontSize)
    {
        OnFontSizeChangedRatio?.Invoke(newFontSize / _currentFontSize);
        _currentFontSize = newFontSize;
    }

    private void HandleAudioVolumeChanged(float value)
    {
        OnAudioVolumeChanged?.Invoke(value);
    }
    #endregion

    #region highscoreMenu Handlers



    #endregion
}
