using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _isPaused = false;
    private bool _isGameOver = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           ExitGame();
        }
    }
    public void GameOver(string text)
    {
        Debug.Log(text);
        _isGameOver = true;
        Time.timeScale = 0f;
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused )
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    private void ExitGame()
    {
        Application.Quit(0);
    }
}
