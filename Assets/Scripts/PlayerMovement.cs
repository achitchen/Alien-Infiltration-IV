using UnityEngine;

// https://www.youtube.com/watch?v=AOVCKEJE6A8&ab_channel=BarthaSzabolcs-GameDevJourney
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // player movespeed
    [SerializeField] private Rigidbody myRb = null;
    [SerializeField] private LayerMask groundMask;

    private Vector3 movement;
    private Vector3 mousePos;
    public Camera cam; // may replace with cinemachine

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        Aim();
    }

    private void FixedUpdate()
    {
        myRb.MovePosition(myRb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private (bool hit, Vector3 pos) GetMousePosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
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
}
