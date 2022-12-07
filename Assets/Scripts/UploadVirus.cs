using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UploadVirus : MonoBehaviour
{
    public bool uploadReady;
    [SerializeField] GameObject virusText;
    void Start()
    {
        uploadReady = false;
    }

    private void Update()
    {
        if (uploadReady)
        {
            if (virusText!= null)
            {
                virusText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.F)) {
                Debug.Log("Start combat phase!");
                SceneManager.LoadScene("Combat_Scene");
            }
        }
        else
        {
            if (virusText!= null)
            {
                virusText.SetActive(false);
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
