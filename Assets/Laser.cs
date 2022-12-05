using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Laser : MonoBehaviour
{

    //[SerializeField] private float range = 15f;
    public LineRenderer lineRenderer;
    public Transform laserTransform;
    public Transform laserFirePos;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        laserTransform = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    public void ShootLaser()
    {
        lineRenderer.SetPosition(0, laserFirePos.position);
        lineRenderer.SetPosition(1, target.position);
    }
}
