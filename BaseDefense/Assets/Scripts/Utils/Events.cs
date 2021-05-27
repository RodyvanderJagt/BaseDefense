using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    public delegate void OnScoreUpdate(int scoreToAdd);
    public delegate void OnDamageToBase(int _damageToBase);
    public delegate void OnGameOver();


    public delegate void OnInvalidTarget(EnemyUnit unit);

    public delegate void OnGamePhaseChanged(GamePhaseManager.GamePhase gamePhase);
    public delegate void OnGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState);
}
