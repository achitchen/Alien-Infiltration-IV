using UnityEngine;

public class PropHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float health = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                Instantiate(effect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 
                    Quaternion.Euler(-90, transform.localEulerAngles.y, transform.localEulerAngles.z));
                Destroy(gameObject, 2);
            }
        }
    }
}
