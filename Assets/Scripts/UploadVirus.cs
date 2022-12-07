using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UploadVirus : MonoBehaviour
{
    public bool uploadReady;
    public bool transitionScene;
    [SerializeField] GameObject virusText;
    [SerializeField] GameManager gameManager;
    void Start()
    {
        uploadReady = false;
        transitionScene = false;
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
                gameManager.LoadCombatScene();
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
        if (other.CompareTag("Player"))
        {
            uploadReady = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uploadReady = false;
        }
    }
}
