using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {Title, Playing, Paused, GameOver}
public enum Difficulty { Easy, Medium, Hard}

public class GameManager : Singleton<GameManager>
{
    public static event Action<Difficulty> OnDifficultyChanged = null;

    public GameState gameState;
    public Difficulty difficulty;
    public int score;
    int scoreMultiplyer = 1;
    float timer;
    private void Start()
    {
        timer = 0;
        Setup();
        OnDifficultyChanged?.Invoke(difficulty);
    }

    private void Update()
    {
        if(gameState == GameState.Playing)
        {
            timer += Time.deltaTime;
            _UI.UpdateTimer(timer);
        }
    }

    void Setup()
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplyer = 1;
                break;
            case Difficulty.Medium:
                scoreMultiplyer = 2;
                break;
            case Difficulty.Hard:
                scoreMultiplyer = 3;
                break;
        }
    }
    
    public void AddScore(int _score)
    {
        score = _score * scoreMultiplyer;
        _UI.UpdateScore(score);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeDifficulty(int _difficulty)
    {
        difficulty = (Difficulty)_difficulty;
        Setup();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyHit += OnEnemyHit;
        Enemy.OnEnemyDie += OnEnemyDie;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHit -= OnEnemyHit;
        Enemy.OnEnemyDie -= OnEnemyDie;
    }

    void OnEnemyHit(GameObject _enemy)
    {
        AddScore(10);
    }

    void OnEnemyDie(GameObject _enemy)
    {
        AddScore(100);
    }
}