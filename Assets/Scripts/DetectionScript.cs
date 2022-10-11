using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    Vector3 lookDirForward;
    Vector3 lookDirLeft;
    Vector3 lookDirRight;
    Vector3 lookDir;
    Collider alienCollider;
    Vector3 alienColliderLeft;
    Vector3 alienColliderRight;
    public float lookAngle = 1.5f;


    private void Start()
    {
        alienCollider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        //CheckForPlayer();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RaycastHit hit;
            lookDirForward = transform.TransformDirection(Vector3.forward);
            lookDir = other.transform.position - this.transform.position;
            Debug.DrawRay(transform.position, lookDir, Color.cyan, 10);
            if (Physics.Raycast(transform.position, lookDir, out hit, 10))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider)
                {
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        Debug.Log("Found you!");
                    }
                    else
                    {
                        Debug.Log("Player is hidden");
                    }
                }

            }
        }
    }
}
