using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=AOVCKEJE6A8&ab_channel=BarthaSzabolcs-GameDevJourney
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // player movespeed
    [SerializeField] private Rigidbody myRb = null;
    [SerializeField] private LayerMask groundMask; // needed?

    private Vector3 movement;

    [SerializeField] private Canvas myUI = null;
    [SerializeField] private Canvas pauseUI = null;
    public bool isPaused = false;

    public Camera cam; // may replace with cinemachine

    private void Awake()
    {
        Application.targetFrameRate = 144; // framerate
    }

    private void Start()
    {
        cam = Camera.main;
        myUI = transform.GetChild(1).GetComponent<Canvas>();
        pauseUI = myUI.transform.GetChild(1).GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        Aim();

        // Pause Input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        
    }

    private void FixedUpdate()
    {
        //myRb.MovePosition(myRb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
            transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
            Debug.DrawRay(transform.position, pos, Color.green);
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

    public void Quit()
    {
        Application.Quit();
        Debug.Log("I quit!");
    }
}
