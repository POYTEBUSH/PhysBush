using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public class RocketEngine : MonoBehaviour
{
    public Vector3 force;

    void FixedUpdate()
    {
        GetComponent<PhysicsObject>().forceContainer.AddForce(force);
    }
}
