using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UploadVirus : MonoBehaviour
{
    public bool uploadReady;
    void Start()
    {
        uploadReady = false;
    }

    private void Update()
    {
        if (uploadReady)
        {
            if (Input.GetKeyDown(KeyCode.F)) {
                Debug.Log("Start combat phase!");
                //SceneManager.LoadScene("CombatScene");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        uploadReady = true;
    }

    private void OnTriggerExit(Collider other)
    {
        uploadReady = false;
    }
}
