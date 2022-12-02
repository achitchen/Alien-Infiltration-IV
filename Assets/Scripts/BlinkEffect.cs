using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    public float timeLimit = 5;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > timeLimit)
        {
            active = !active;
            gameObject.GetComponent<Text>().enabled = active;
            timer = 0;
        }
    }
}
