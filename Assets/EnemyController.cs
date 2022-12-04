using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform target;
    public float speed = 1f;
    public Rigidbody rb;

    public float timer = 0, timeLimit = 5;
    private bool shooting = false;
    public GameObject laserBeam = null;
    public Transform attackPoint = null;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            if (!shooting)
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

    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(90, transform.position.y, angle - 90);
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        shooting = false;
    }
}


