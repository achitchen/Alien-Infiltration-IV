using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public GameManager gMan;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Victory());
        }
    }

    private IEnumerator Victory()
    {
        Debug.Log("YOU WIN!");
        Animator transitionAnim = gMan.player.transform.GetChild(1).transform.GetChild(3).GetComponent<Animator>();
        transitionAnim.SetBool("Ending", true);
        yield return new WaitForSeconds(1f);
        gMan.player.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).transform.gameObject.SetActive(true);
        gMan.player.transform.GetChild(1).transform.GetChild(3).transform.GetChild(1).transform.gameObject.SetActive(true);
    }
}
