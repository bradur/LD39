// Date   : 30.07.2017 22:53
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static GameManager main;

    private bool gameOver = false;
    public bool GameIsOver { get { return gameOver; } }

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 0)
        {
            main = this;
            gameObject.tag = "GameManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void GameOver()
    {
        UIManager.main.ShowGameOverScreen();
        gameOver = true;
        Time.timeScale = 0f;
    }

    public void TheEnd()
    {
        UIManager.main.ShowTheEndScreen();
        gameOver = true;
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void Start () {
    
    }

    void Update () {
        if (gameOver)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Exit();
            } else if (Input.GetKeyUp(KeyCode.R))
            {
                Restart();
            }
        }
    }
}
