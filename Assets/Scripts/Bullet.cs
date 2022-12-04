using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject colEffect = null;

    private void Start()
    {
        Destroy(gameObject, 5f); // destroys after 'x' amount of seconds regardless of collision
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z); // maintain local rotation
    }

    private void OnCollisionEnter(Collision collision) // if you toggle off 'Freeze Position Y' then you can shoot within the bounds of the Y-Axis
    {
        GameObject effect = Instantiate(colEffect, transform.position, Quaternion.Euler(90, transform.localEulerAngles.y, transform.localEulerAngles.z));
        Destroy(effect, 0.2f);
        Destroy(gameObject); // destroy the bullet
    }
}
