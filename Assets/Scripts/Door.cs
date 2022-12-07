using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject exitPos = null;
    [SerializeField] private GameObject camPos = null;
    [SerializeField] private GameObject camHolder = null;
    public GameManager gMan;

    // Start is called before the first frame update
    void Start()
    {
        exitPos = gameObject.transform.GetChild(0).gameObject;
        camPos = gameObject.transform.GetChild(1).gameObject;
        camHolder = GameObject.Find("CameraHolder");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (exitPos == null || camPos == null || camHolder == null)
            {
                exitPos = gameObject.transform.GetChild(0).gameObject;
                camPos = gameObject.transform.GetChild(1).gameObject;
                camHolder = GameObject.Find("CameraHolder");
            }
            StartCoroutine(RoomTransition(other));
        }
    }

    private IEnumerator RoomTransition(Collider player)
    {
        Animator transitionAnim = gMan.player.transform.GetChild(1).transform.GetChild(3).GetComponent<Animator>();
        transitionAnim.SetTrigger("Transition");
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(exitPos.transform.position.x, gMan.player.transform.position.y, exitPos.transform.position.z);
        camHolder.transform.position = new Vector3(camPos.transform.position.x, camPos.transform.position.y, camPos.transform.position.z);
        transitionAnim.SetTrigger("Transition");
    }
}
