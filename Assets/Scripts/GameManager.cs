using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool stealthState;
    public bool transitionState;
    public bool isGameOver;
    [SerializeField] AudioClip stealthMusic;

    // Checkpoints
    [Header("Checkpoints")]
    public GameObject currentCheckpoint;
    public GameObject player = null;

    #region Singleton & Awake
    public static GameManager gMan = null; // should always initilize

    private void Awake()
    {
        if (gMan == null)
        {
            DontDestroyOnLoad(gameObject);
            gMan = this;
        }
        else if (gMan != null)
        {
            Destroy(gameObject); // if its already there destroy it
        }
        Application.targetFrameRate = 144; // framerate
    }
    #endregion

    void Start()
    {
        AudioSource musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = 0.7f;
        musicSource.clip = stealthMusic;
        musicSource.Play();
        Time.timeScale = 1;
        isGameOver = false;
        stealthState = true;
        player = GameObject.FindGameObjectWithTag("Player");
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

    public void Quit()
    {
        Application.Quit();
        Debug.Log("I quit!");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void RespawnAtLastCheckpoint()
    {
        if (currentCheckpoint != null)
            player.gameObject.transform.position = currentCheckpoint.transform.position;
    }
}
