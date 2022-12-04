using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private int damage = 1;
    public Image healthBar = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        //GameManager.gMan.player.GetComponent<PlayerController>().health -= damage;
        //healthBar.GetComponent<RectTransform>().right = damage;

    }
}
