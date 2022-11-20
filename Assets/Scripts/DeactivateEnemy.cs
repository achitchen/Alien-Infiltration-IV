using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateEnemy : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.transitionState == true)
        {
            gameObject.SetActive(false);
        }
    }
}
