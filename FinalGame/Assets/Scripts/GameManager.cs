using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded;
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;
    public int SPtoWin;

    private void Start()
    {
        gameEnded = false;
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (PlayerStats.skillPoints >= SPtoWin)
        {
            WinLevel();
        }
        
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (PlayerStats.skillPoints < 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        levelCompleteUI.SetActive(true);
        gameEnded = true;
    }
}
