using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform target;
    public float speed = 1f;
    public Rigidbody rb;
    public float timer = 0, timeLimit = 5;
    private bool shooting = false, preparingToShoot = false;
    public LineRenderer laserBeam = null;
    public Transform attackPoint = null;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       laserBeam.enabled = false;
    }

    private void FixedUpdate()
    {
        laserBeam.SetPosition(0, attackPoint.position);

        if (shooting)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                Debug.Log("I'm firing my laser!");

                laserBeam.SetPosition(1, new Vector3(hit.point.x, hit.point.y, hit.point.z));
                if (hit.collider.gameObject == GameManager.gMan.player)
                {
                    Debug.Log("I GOT U");
                    GameManager.gMan.playerController.TakeDamage(1);
                    
                }
            }
        }


        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            if (!preparingToShoot)
            {
                MoveTowardsTarget();
            }
            RotateTowardsTarget();
        }

        if (!shooting)
            timer += Time.deltaTime;

        if (timer > timeLimit)
        {
            StartCoroutine(Shoot());
            timer = 0;
        }

        
        //laserBeam.SetPosition(1, target.position);
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void RotateTowardsTarget()
    {
        //Vector3 direction = target.position - transform.position;
        //var angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = Quaternion.Euler(90, transform.position.y, angle - 90);


        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.fixedDeltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(90, rotation.y, transform.rotation.z);
    }

    private IEnumerator Shoot()
    {
        preparingToShoot = true;
        yield return new WaitForSeconds(1f);
        shooting = true;
        laserBeam.enabled = true;
        yield return new WaitForSeconds(1f);
        laserBeam.enabled = false;
        preparingToShoot = false;
        shooting = false;
    }

}


