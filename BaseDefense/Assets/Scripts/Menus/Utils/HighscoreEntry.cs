using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _scoreText;

    public string NameText
    {
        set { _nameText.text = value; }
    }

    public int ScoreText
    {
        set { _scoreText.text = value.ToString(); }
    }

}
