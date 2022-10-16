using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject colEffect = null;

    private void Start()
    {
        Destroy(gameObject, 5f); // destroys after 'x' amount of seconds regardless of collision
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(colEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f); // destroy the effect
        Destroy(gameObject); // destroy the bullet
    }
}
