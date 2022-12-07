using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool stealthState;
    public bool transitionState;
    public bool isGameOver;
    [SerializeField] AudioClip musicClip;
    private GameObject alienMainframe;

    // Checkpoints
    [Header("Checkpoints")]
    public GameObject currentCheckpoint;
    public GameObject player = null;
    public PlayerController playerController = null;

    AudioSource musicSource;
    [SerializeField] AudioClip enemyRoar;
    [SerializeField] AudioClip enemyRoar2;
    private AudioSource enemySounds;

    #region Singleton & Awake
    //public static GameManager gMan = null; // should always initilize

    //private void Awake()
    //{
    //    if (gMan == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        gMan = this;
    //    }
    //    else if (gMan != null)
    //    {
    //        Destroy(gameObject); // if its already there destroy it
    //    }
    //    Application.targetFrameRate = 144; // framerate
    //}
    #endregion

    void Start()
    {
        
        enemySounds = gameObject.AddComponent<AudioSource>();
        enemySounds.volume = 0.65f;
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.volume = 0.7f;
            musicSource.clip = musicClip;
        }
        if (alienMainframe == null)
        {
            if (SceneManager.GetActiveScene().name != "Main_Menu")
            {
                alienMainframe = GameObject.Find("AlienMainframe");
            }
        }
        musicSource.Play();
        Time.timeScale = 1;
        isGameOver = false;
        stealthState = true;
        InitialiseGame();
        Application.targetFrameRate = 144; // framerate
    }

    public void InitialiseGame()
    {
        if (SceneManager.GetActiveScene().name != "Main_Menu")
        {
            player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<PlayerController>();

            player.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(false);
            Debug.Log("Loading assets");
        }
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                if (musicSource.isPlaying)
                {
                    musicSource.Stop();
                }
                //Awake();
                //Start();
            }
        }
    }
    public void GameOver()
    {
        player.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true); // dead image
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
        StartCoroutine(LoadTime());
    }

    public void LoadMenu()
    {
        playerController.Pause();
        SceneManager.LoadScene("Main_Menu"); // for the purpose of time :(
    }

    private IEnumerator LoadTime()
    {
        yield return new WaitForSeconds(1f);
        InitialiseGame();
    }

    public void RespawnAtLastCheckpoint()
    {
        if (currentCheckpoint != null)
            player.gameObject.transform.position = currentCheckpoint.transform.position;
    }

    public void LoadCombatScene()
    {
        {
            SceneManager.LoadScene("Combat_Scene");
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            
            //Awake();
            //Start();
        }
    }

    public void AlienRoar()
    {
        enemySounds.PlayOneShot(enemyRoar, 0.7f);
    }
}
