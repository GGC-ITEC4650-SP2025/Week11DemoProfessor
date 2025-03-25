using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    public Vector3 kickVec;
    Rigidbody myBod;
    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody>();
        myBod.velocity = kickVec;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
