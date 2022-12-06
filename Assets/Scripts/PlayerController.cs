using UnityEngine;
using UnityEngine.EventSystems;

// https://www.youtube.com/watch?v=AOVCKEJE6A8&ab_channel=BarthaSzabolcs-GameDevJourney
public class PlayerController : MonoBehaviour
{
    [Header("Player Traits")]
    [SerializeField] private float moveSpeed = 10f; // player movespeed
    public int currHealth = 0, maxHealth = 500; 
    public PlayerHealth healthBar;
    private Rigidbody myRb = null;
    private Vector3 movement;

    [Header("UI")]
    [SerializeField] private Canvas myUI = null;
    [SerializeField] private Canvas pauseUI = null;
    public bool isPaused = false;

    [Header("Camera Properties")]
    public Camera cam; // may replace with cinemachine

    private void Start()
    {
        cam = Camera.main;
        myUI = transform.GetChild(1).GetComponent<Canvas>();
        pauseUI = myUI.transform.GetChild(1).GetComponent<Canvas>();
        myRb = GetComponent<Rigidbody>();

        // Health + UI
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        myUI = transform.GetChild(1).GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0)
        {
            // player dies
            // game is over
            Debug.Log("GAME OVER");
        }

        if (!isPaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
            Aim();
        }
        
        // Pause Input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void FixedUpdate()
    {
        myRb.velocity = new Vector3(movement.x, 0, movement.z).normalized * moveSpeed * Time.deltaTime; // new movement so that the player does not phase through walls
    }

    private (bool hit, Vector3 pos) GetMousePosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            return (hit: true, pos: hitInfo.point);
        }
        else
        {
            return (hit: false, pos: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (hit, pos) = GetMousePosition();
        if (hit)
        {
            var direction = pos - transform.position;
            direction.y = 0;
            transform.up = direction;
            transform.localEulerAngles = new Vector3(-90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    public void Pause() // move to elsweyr?
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseUI.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            pauseUI.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }
}
