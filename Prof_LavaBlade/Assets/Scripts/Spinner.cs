using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 spinVec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles += spinVec * Time.deltaTime;
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        transform.eulerAngles += spinVec * Time.fixedDeltaTime;
    }

}
