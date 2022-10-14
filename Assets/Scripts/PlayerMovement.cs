using UnityEngine;

// https://www.youtube.com/watch?v=LNLVOjbrQj4&ab_channel=Brackeys
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // player movespeed
    [SerializeField] private Rigidbody2D myRb = null;

    private Vector2 movement;
    private Vector2 mousePos;
    public Camera cam; // may replace with cinemachine

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        myRb.MovePosition(myRb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - myRb.position; // player targets cursor
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        myRb.rotation = angle;
    }
}
