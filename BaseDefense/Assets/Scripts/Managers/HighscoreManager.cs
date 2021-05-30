using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Highscore : IComparable
{
    [SerializeField] private int _score;
    [SerializeField] private string _name;

    public int Score => _score;
    public string Name => _name;

    public Highscore(int score, string name)
    {
        _score = score;
        _name = name;
    }

    public int CompareTo(object obj)
    {
        if (obj.GetType().Equals(typeof(Highscore)))
        {
            Highscore otherscore = (Highscore)obj;
            return _score.CompareTo(otherscore.Score);
        }
        return 0;
    }
}

public class HighscoreManager : Singleton<HighscoreManager>
{
    [SerializeField] List<Highscore> _highscores = new List<Highscore>();
    [SerializeField] int _maxLength = 7;

    public List<Highscore> Highscores => _highscores;

    public int HighestScore
    {
        get
        {
            if (_highscores != null)
            {
                return _highscores[0].Score;
            }
            return 0;
        }
    }

    public void UpdateHighScore(int score)
    {
        if (score != 0)
        {
            _highscores.Add(new Highscore(score, UIManager.Instance.PlayerName));
            _highscores.Sort();

            while (_highscores.Count > _maxLength)
            {
                _highscores.RemoveAt(0);
            }

            _highscores.Reverse();
        }
    }

}
