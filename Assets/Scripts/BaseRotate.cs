using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(4f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
