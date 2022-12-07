using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Traits")]
    [SerializeField] private float moveSpeed = 1f; // player movespeed
    public int currHealth = 0, maxHealth = 100;
    public Rigidbody rb;
    public float timer = 0, shootTimeLimit = 5;
    private bool shooting = false, preparingToShoot = false, following = false;
    public LineRenderer laserBeam = null;
    public Transform attackPoint = null;
    [SerializeField] AudioClip laserSound;
    private AudioSource enemySounds;

    [Header("UI")]
    public EnemyHealth healthBar;

    [Header("Player References")]
    public Transform target;

    [Header("Other")]
    private const int propLayer = 9;
    public GameManager gMan;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       laserBeam.enabled = false;
        enemySounds = gameObject.AddComponent<AudioSource>();
        enemySounds.volume = 0.65f;

        // Health + UI
        currHealth = maxHealth;
       healthBar.SetMaxHealth(maxHealth);
    }

    private void FixedUpdate()
    {
        laserBeam.SetPosition(0, attackPoint.position); // set the position of the laserbeam

        if (currHealth <= 0) // check health
        {
            gMan.AlienRoar();
            Destroy(gameObject);
        }

        // flip sprite
        if (transform.localRotation.z < target.position.x)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
            attackPoint.localPosition = new Vector3(attackPoint.localPosition.x, -0.5f, attackPoint.localPosition.z);
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            attackPoint.localPosition = new Vector3(attackPoint.localPosition.x, 0.5f, attackPoint.localPosition.z);
        }

        if (shooting) // hit-scan
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                laserBeam.SetPosition(1, new Vector3(hit.point.x, hit.point.y, hit.point.z)); // set the end position to the hit position

                if (hit.collider.gameObject == gMan.player) // if hit player
                {
                    gMan.playerController.TakeDamage(1);
                }
                if (hit.collider.gameObject.layer == propLayer) // if hit prop
                {
                    hit.collider.gameObject.GetComponent<PropHealth>().health--;
                }
            }
        }

        if (Vector3.Distance(transform.position, target.position) < 50f) // distance from player
        {
            if (!preparingToShoot)
            {
                MoveTowardsTarget();
            }
            RotateTowardsTarget();
        }
        else
        {
            following = false;
        }

        if (!shooting && following) // timer
            timer += Time.deltaTime;

        if (timer > shootTimeLimit)
        {
            StartCoroutine(Shoot());
            timer = 0;
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        following = true;
    }

    private void RotateTowardsTarget()
    {
        var direction = target.position - transform.position;
        direction.y = 0;
        transform.up = direction;
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private IEnumerator Shoot()
    {
        preparingToShoot = true;
        yield return new WaitForSeconds(0.3f);
        shooting = true;
        enemySounds.PlayOneShot(laserSound, 0.7f);
        yield return new WaitForSeconds(0.1f);
        laserBeam.enabled = true;
        yield return new WaitForSeconds(0.3f);
        laserBeam.enabled = false;
        preparingToShoot = false;
        shooting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currHealth -= 10;
            healthBar.SetHealth(currHealth);
        }
    }
}
