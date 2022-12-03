using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject exitPos = null;
    [SerializeField] private GameObject camPos = null;
    [SerializeField] private Camera cam = null;

    // Start is called before the first frame update
    void Start()
    {
        exitPos = gameObject.transform.GetChild(0).gameObject;
        camPos = gameObject.transform.GetChild(1).gameObject;
        cam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(RoomTransition(other));
        }
    }

    private IEnumerator RoomTransition(Collider player)
    {
        Animator transitionAnim = GameManager.gMan.player.transform.GetChild(1).transform.GetChild(3).GetComponent<Animator>();
        transitionAnim.SetTrigger("Transition");
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(exitPos.transform.position.x, GameManager.gMan.player.transform.position.y, exitPos.transform.position.z);
        cam.transform.position = new Vector3(camPos.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        transitionAnim.SetTrigger("Transition");
    }
}
