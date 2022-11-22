using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool stealthState;
    public bool transitionState;
    public bool isGameOver;
    void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;
        stealthState = true;
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        //Activate GameOver Screen
    }
}
