using UnityEngine;

public class Shooting : MonoBehaviour // replaced with 'Guns' script
{
    [SerializeField] private Transform firePos = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // may replace with new input system
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation); // create the bullet at 'firePos'
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(firePos.up * bulletForce, ForceMode.Impulse); // add force to the bullet
    }
}
