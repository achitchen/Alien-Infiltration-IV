using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float minMovementDistance = 1f;
    [SerializeField] float minMovementTime = 1f;
    [SerializeField] float maxMovementTime = 1f;
    [SerializeField] AudioClip moveSound;
    private AudioSource moveSource;
    private SpriteRenderer spriteRenderer;
    Vector3 movementDir;
    bool isDetecting;
    [SerializeField] bool isMoving;
    void Start()
    {
        movementDir = Vector3.zero;
        isDetecting = false;
        spriteRenderer = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        moveSource = gameObject.AddComponent<AudioSource>();
        moveSource.volume = 0.1f;
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), GetComponent<Collider>());
    }

    void FixedUpdate()
    {
        var detectionTimer = GetComponent<DetectionScript>().detectionTimer;
        if (detectionTimer > 0)
        {
            isDetecting = true;
            if (isMoving)
            {
                CancelMovement();
                CancelInvoke();
            }
        }
        else
        {
            isDetecting = false;
        }

        if (movementDir == Vector3.zero && !isMoving)
        {
            Invoke("ChooseMovementDir", 1f);
            isMoving = true;
        }
        else if (movementDir != Vector3.zero)
        {
            Movement(movementDir);
        }
    }

    private void ChooseMovementDir()
    {
        if (!isDetecting)
        {
            int index = Random.Range(0, 4);

            switch (index)
            {
                case 0:
                    movementDir = Vector3.forward;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case 1:
                    movementDir = Vector3.left;
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
                    spriteRenderer.flipX = true;
                    break;
                case 2:
                    movementDir = Vector3.back;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case 3:
                    movementDir = Vector3.right;
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
                    spriteRenderer.flipX = false;
                    break;
                case 4: movementDir = Vector3.zero;
                    break;
            }
            isMoving = true;
            moveSource.PlayOneShot(moveSound, 0.35f);
            Invoke("CancelMovement", Random.Range(1.5f, 3.5f));
        }
    }

    private void Movement(Vector3 dir)
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        RaycastHit hit;
        Debug.DrawRay(transform.position, movementDir, Color.green, minMovementDistance);
        if (Physics.Raycast(transform.position, movementDir, out hit, minMovementDistance))
        {
            if (hit.transform.gameObject.tag != "Ground")
            {
                CancelMovement();
                CancelInvoke();
            }
        }
    }

    private void CancelMovement()
    {
        movementDir = Vector3.zero;
        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            CancelMovement();
            CancelInvoke();
        }
    }
}
