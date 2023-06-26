using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static GameManager Instance { get; private set; }
    private State state;
    private float waitingToStart = 1f;
    private float countDown = 3f;
    private float gamePlaying;
    private float gamePlayingMax = 10f;
    private bool IsGamePaused;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
        gamePlaying = gamePlayingMax;
    }

    private void Start()
    {
        GameInput.Instance.OnPausePerformed += OnPausePressed;
    }

    public void OnPausePressed(object sender, EventArgs e)
    {
      TogglePause();
    }

    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        }
        else
        {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1;
        }
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStart -= Time.deltaTime;
                if (waitingToStart < 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.CountdownToStart:
                countDown -= Time.deltaTime;
                if (countDown < 0)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GamePlaying:
                gamePlaying -= Time.deltaTime;
                if (gamePlaying < 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameCountingDown()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countDown;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetTimeLeftPercentage()
    {
        return gamePlaying / gamePlayingMax;
    }
}