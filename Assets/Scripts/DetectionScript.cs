using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    Vector3 lookDir;

    private void FixedUpdate()
    {
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        lookDir = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, lookDir, 10)) 
        {
            print("I see you!");
        }
    }
}
