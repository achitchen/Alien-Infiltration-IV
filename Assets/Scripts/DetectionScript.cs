using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    Vector3 lookDirForward;
    Vector3 lookDir;
    GameObject playerObject;
    bool canDetect;
    bool playerSeen;
    public float lookAngle = 1.5f;
    [SerializeField] float detectionTimer = 0f;
    [SerializeField] float alertedTime = 100f;
    [SerializeField] GameObject detectionSprite;
    private Color detectionSpriteColor;


    private void Start()
    {
        canDetect = false;
        playerSeen = false;
        detectionSpriteColor = Color.yellow;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if(canDetect && playerObject != null)
        {
            CheckForPlayer(playerObject);
        }

        if (playerSeen)
        {
            StopCoroutine(ResetAlarm());
            if (detectionTimer < alertedTime)
            {
                detectionTimer += 1f;
            }
            else
            {
                {
                    Debug.Log("Kill player");
                }
                
            }
        }

        if (!playerSeen && detectionTimer > 0)
        {
            StartCoroutine(ResetAlarm());
        }

        detectionSpriteColor = new Color(detectionSpriteColor.r, detectionSpriteColor.g, detectionSpriteColor.b, (detectionTimer / 100));
        detectionSprite.GetComponent<SpriteRenderer>().color = detectionSpriteColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canDetect = true;
            playerObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canDetect = false;
            playerObject = null;
            playerSeen = false;
        }
    }

    private void CheckForPlayer(GameObject other)
    {
        RaycastHit hit;
        lookDirForward = transform.TransformDirection(Vector3.forward);
        lookDir = other.transform.position - this.transform.position;
        Debug.DrawRay(transform.position, lookDir, Color.cyan, 10);
        if (Physics.Raycast(transform.position, lookDir, out hit, 10))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    Debug.Log("Found you!");
                    playerSeen = true;
                }
                else
                {
                    Debug.Log("Player is hidden");
                    playerSeen = false;
                }
            }
        }
    }

    IEnumerator ResetAlarm()
    {
        yield return new WaitForSeconds(2f);
        detectionTimer = 0f;
        playerSeen = false;
    }
}

