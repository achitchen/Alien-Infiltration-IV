using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRoomScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.stealthState = false;
            gameManager.transitionState = true;
        }
    }
}
