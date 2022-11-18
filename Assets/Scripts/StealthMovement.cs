using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float minMovementDistance = 1f;
    Vector3 movementDir;
    bool isDetecting;
    [SerializeField] bool isMoving;
    void Start()
    {
        movementDir = Vector3.zero;
        isDetecting = false;
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
                    break;
                case 1:
                    movementDir = Vector3.left;
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    break;
                case 2:
                    movementDir = Vector3.back;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case 3:
                    movementDir = Vector3.right;
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case 4: movementDir = Vector3.zero;
                    break;
            }
            isMoving = true;
            Invoke("CancelMovement", Random.Range(1.5f, 3.5f));
        }

        RaycastHit hit;
        Debug.DrawRay(transform.position, movementDir, Color.green, minMovementDistance);
        if (Physics.Raycast(transform.position, movementDir, out hit, minMovementDistance))
        {
            CancelMovement();
            CancelInvoke();
        }
    }

    private void Movement(Vector3 dir)
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void CancelMovement()
    {
        movementDir = Vector3.zero;
        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CancelMovement();
        CancelInvoke();
    }
}
