using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public Vector3 velocity; //average velocity this fixed update.

    void FixedUpdate()
    {
        Vector3 deltaS = velocity * Time.deltaTime;

        transform.position += deltaS;
    }
}
