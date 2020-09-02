using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrag : MonoBehaviour
{
    //Stokes Law
    [Range (1f, 2f)]
    public float velocityExponent; // [none]

    public float dragConstant; // [??]

    private PhysicsObject physicsObject; // Physics object component attached to this scene object.

    void Start()
    {
        physicsObject = GetComponent<PhysicsObject>();
    }

    void FixedUpdate()
    {
        float speed = physicsObject.speed;
        float dragSize = CalculateDrag(speed);
        Vector3 dragForce = dragSize * -physicsObject.velocity.normalized;

        physicsObject.forceContainer.AddForce(dragForce);
    }

    float CalculateDrag(float speed)
    {
        return dragConstant * Mathf.Pow(speed, velocityExponent);
    }
}
