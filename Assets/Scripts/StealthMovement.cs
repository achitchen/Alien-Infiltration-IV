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
        else
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
                    break;
                case 1:
                    movementDir = Vector3.left;
                    break;
                case 2:
                    movementDir = Vector3.back;
                    break;
                case 3:
                    movementDir = Vector3.right;
                    break;
                case 4: movementDir = Vector3.zero;
                    break;
            }
            Invoke("CancelMovement", Random.Range(2f, 4f));
        }

        RaycastHit hit;
        Debug.DrawRay(transform.position, movementDir, Color.green, minMovementDistance);
        if (Physics.Raycast(transform.position, movementDir, out hit, minMovementDistance))
        {
            CancelMovement();
        }
    }

    private void Movement(Vector3 dir)
    {
        transform.Translate(dir * movementSpeed * Time.deltaTime);
    }

    private void CancelMovement()
    {
        movementDir = Vector3.zero;
        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CancelMovement();
    }
}
