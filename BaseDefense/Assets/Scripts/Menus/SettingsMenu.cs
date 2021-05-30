using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu
{
    [SerializeField] Button _backButton;


    [SerializeField] TMP_InputField _playerNameInputField;

    [Header("Font size")]
    [SerializeField] Button _smallFontButton;
    [SerializeField] Button _mediumFontButton;
    [SerializeField] Button _largeFontButton;
    [SerializeField] float _smallFontSize = 0.75f;
    [SerializeField] float _mediumFontSize = 1.0f;
    [SerializeField] float _largeFontSize = 1.25f;

    [Header("Audio")]
    [SerializeField] Slider _audioVolumeSlider;
    [SerializeField] TextMeshProUGUI _audioVolumeText;

    public event Action OnBackButtonClicked;
    public event Action<string> OnPlayerNameChanged;
    public event Action<float> OnFontSizeChanged;
    public event Action<float> OnAudioVolumeChanged;

    private void Start()
    {
        _backButton.onClick.AddListener(HandleBackButtonClicked);
        _playerNameInputField.onEndEdit.AddListener(HandlePlayerNameChanged);
        _smallFontButton.onClick.AddListener(HandleSmallFontClicked);
        _mediumFontButton.onClick.AddListener(HandleMediumFontClicked);
        _largeFontButton.onClick.AddListener(HandleLargeFontClicked);
        _audioVolumeSlider.onValueChanged.AddListener(HandleAudioVolumeChanged);

    }

    private void HandleBackButtonClicked()
    {
        OnBackButtonClicked?.Invoke();
    }

    private void HandlePlayerNameChanged(string playerName)
    {
        OnPlayerNameChanged?.Invoke(playerName);
    }

    private void HandleSmallFontClicked()
    {
        OnFontSizeChanged?.Invoke(_smallFontSize);
    }

    private void HandleMediumFontClicked()
    {
        OnFontSizeChanged?.Invoke(_mediumFontSize);
    }
    private void HandleLargeFontClicked()
    {
        OnFontSizeChanged?.Invoke(_largeFontSize);
    }
    private void HandleAudioVolumeChanged(float audioVolume)
    {
        OnAudioVolumeChanged?.Invoke(audioVolume);
        _audioVolumeText.text = audioVolume + "%";
    }
}


