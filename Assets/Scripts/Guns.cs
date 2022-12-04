using UnityEngine;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=wZ2UUOC17AY&t=462s&ab_channel=Dave%2FGameDevelopment
public class Guns : MonoBehaviour
{
    // player
    public PlayerController playerController = null;

    //bullet 
    [SerializeField] private GameObject bullet = null;

    //bullet force
    [SerializeField] private float shootForce, upwardForce;

    //Gun stats
    [SerializeField] private float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;

    private int bulletsLeft, bulletsShot;

    //Recoil
    [SerializeField] private Rigidbody playerRb = null;
    [SerializeField] private float recoilForce;

    //bools
    private bool shooting, readyToShoot, reloading;

    //Reference
    [SerializeField] private Camera cam = null;
    [SerializeField] private Transform attackPoint = null;

    //Graphics
    [SerializeField] private Text ammunitionDisplay = null;

    //bug fixing :D
    [SerializeField] private bool allowInvoke = true;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!playerController.isPaused)
            FireInput(); // Fire only if cursor is over 'groundMask'?

        //Set ammo display, if it exists :D
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.text = (bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }
    private void FireInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        //Find the exact hit position using a raycast
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        var distance = Vector3.Distance(transform.position, hit.point); // safe zone
        if (distance > 2)
        {
            readyToShoot = false;

            //Calculate spread
            float x = Random.Range(-spread, spread);
            float z = Random.Range(-spread, spread);

            //Calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, 0, z); //Just add spread to last direction

            //Instantiate bullet/projectile
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
                                                                                                       //Rotate bullet to shoot direction
            currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);
            //Debug.Log(directionWithSpread.normalized * shootForce);
            bulletsLeft--;
            bulletsShot++;

            //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
            if (allowInvoke)
            {
                Invoke("ResetShot", timeBetweenShooting);
                allowInvoke = false;

                //Add recoil to player (should only be called once)
                playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
            }

            //if more than one bulletsPerTap make sure to repeat shoot function
            if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
                Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
