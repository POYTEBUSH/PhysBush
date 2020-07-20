using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public Vector3 velocity; // The average velocity this fixed update.

    public Vector3 netForce; // The net force applied to this physics object.
    public List<Vector3> forces; // All the forces currently applied to this object.

    void FixedUpdate()
    {
        CalculateNetForce();

        if (netForce == Vector3.zero)
        {            
            Vector3 deltaS = velocity * Time.deltaTime;
            transform.position += deltaS;
        }
        else
        {
            Debug.LogError("Unbalanced force detected. FUCK");
        }
    }

    private void CalculateNetForce()
    {
        Vector3 totalForce = forces[0];
        for (int i = 1; i < forces.Count; i++)
        {
            totalForce += forces[i];
        }
        netForce = totalForce / forces.Count;
    }
}
