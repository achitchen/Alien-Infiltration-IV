using UnityEngine;
using EZCameraShake;

public class PropHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    public float health = 5;
    private bool destroyed = false;

    private void Update()
    {
        if (health <= 0 && !destroyed)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Instantiate(effect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            Quaternion.Euler(-90, transform.localEulerAngles.y, transform.localEulerAngles.z));
            CameraShaker.Instance.ShakeOnce(4f, 3f, 0.5f, 0.5f);
            destroyed = true;
            Destroy(gameObject, 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
        }
    }
}
