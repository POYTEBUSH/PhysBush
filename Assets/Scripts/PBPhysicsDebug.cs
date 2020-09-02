using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PBPhysicsDebug : MonoBehaviour
{
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Format("Name: {0} | Inertia Tensor: {1} | Centre Of Mass: {2}", gameObject.name, rigidBody.inertiaTensor, rigidBody.centerOfMass));
    }
}
