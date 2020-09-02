using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour
{
	private PhysicsObject[] physicsObjects;
    
    [Tooltip("Are there any dynamic physics objects or are all added on start.")]
    public  bool dynamicObjects = false;

    void Start()
    {
		physicsObjects = FindObjectsOfType<PhysicsObject>();        
    }

    void FixedUpdate()
    {        
		CalculateGravity();
    }

    void CalculateGravity()
    {
        if (dynamicObjects)
        {
            physicsObjects = FindObjectsOfType<PhysicsObject>();
        }

        foreach (PhysicsObject physicsObjectA in physicsObjects)
        {
			foreach (PhysicsObject physicsObjectB in physicsObjects)
			{
				if ((physicsObjectA != physicsObjectB) && (physicsObjectA != this))
                {
					Debug.Log("Calulating the force applied to " + physicsObjectA.name + " relating to the gravity of " + physicsObjectB.name);

					const float gravitationalConstant = 6.673e-11f; // [m^3 s^-2 kg^-1]

					Vector3 offset = physicsObjectA.transform.position - physicsObjectB.transform.position;
					float rSquared = Mathf.Pow(offset.magnitude, 2.0f);
					float gravityMagnitude = (gravitationalConstant * ((physicsObjectA.mass * physicsObjectB.mass) / rSquared));

					Vector3 graviationalForce = gravityMagnitude * offset.normalized;

					physicsObjectA.forceContainer.AddForce(-graviationalForce);
                }
			}
        }
    }
}
