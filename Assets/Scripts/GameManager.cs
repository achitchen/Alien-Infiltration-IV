using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool stealthState;
    public bool transitionState;
    // Start is called before the first frame update
    void Start()
    {
        stealthState = true;
    }
}
