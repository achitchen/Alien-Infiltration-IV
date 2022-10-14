using UnityEngine;

public class Shooting : MonoBehaviour
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
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePos.up * bulletForce, ForceMode2D.Impulse); // add force to the bullet
    }
}
