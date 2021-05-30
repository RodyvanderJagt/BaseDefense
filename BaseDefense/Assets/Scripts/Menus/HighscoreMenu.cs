using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class HighscoreMenu : Menu
{
    [SerializeField] Button _backButton;
    HighscoreEntry[] _highscoreEntries;

    public event Action OnBackButtonPressed;

    private void Start()
    {
        _backButton.onClick.AddListener(HandleBackButtonClicked);
        _highscoreEntries = GetComponentsInChildren<HighscoreEntry>();
        GetHighScores();
    }
    private void OnEnable()
    {
        if (_highscoreEntries != null)
        {
            GetHighScores();
        }
    }

    private void GetHighScores()
    {
        if (HighscoreManager.Instance == null)
        {
            return;
        }

        int index = 0;
        List<Highscore> highscores = HighscoreManager.Instance.Highscores;
        while (index < highscores.Count && index < _highscoreEntries.Length)
        {
            _highscoreEntries[index].ScoreText = highscores[index].Score;
            _highscoreEntries[index].NameText = highscores[index].Name;
            index++;
        }
    }

    private void HandleBackButtonClicked()
    {
        OnBackButtonPressed?.Invoke();
    }

}
