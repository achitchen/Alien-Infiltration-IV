using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    // Point Enums
    public enum Landmarks
    {
        Checkpoint,
        StartGame,
        EndGame,
    }

    public Landmarks point;
    [SerializeField] private Material checkpointActived = null;

    private void OnTriggerEnter(Collider collision)
    {
        if (point == Landmarks.Checkpoint)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.gMan.currentCheckpoint = gameObject;
                gameObject.transform.GetComponent<Renderer>().material = checkpointActived;
                Destroy(this); // only keeps last checkpoint
            }
        }
        
    }
}

