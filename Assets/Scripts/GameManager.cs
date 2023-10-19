using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        waittingToStart,
        countingToStart,
        arePlaying,
        gameOver
    }

    private State state;
    private float waittingToStartTimer = 1;
    private float countingToStartTimer = 3;
    private float arePlayingTimer;
    private float arePlayingTimerMax = 15;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.waittingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPaused += GameInput_OnPaused;
    }

    private void GameInput_OnPaused(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.waittingToStart:
                waittingToStartTimer -= Time.deltaTime;
                if (waittingToStartTimer <= 0)
                {
                    state = State.countingToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.countingToStart:
                countingToStartTimer -= Time.deltaTime;
                if (countingToStartTimer <= 0)
                {
                    state = State.arePlaying;
                    arePlayingTimer = arePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.arePlaying:
                arePlayingTimer -= Time.deltaTime;
                if (arePlayingTimer <= 0)
                {
                    state = State.gameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }

    }

    public bool arePlaying()
    {
        return state == State.arePlaying;
    }

    public bool isCountingToStart()
    {
        return state == State.countingToStart;
    }

    public float GetCountingToStartTimer()
    {
        return countingToStartTimer;
    }

    public bool isGameOver()
    {
        return state == State.gameOver;
    }

    public float GetPlayingTimerNomalized()
    {
        return 1 - (arePlayingTimer / arePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
