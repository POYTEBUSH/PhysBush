﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{    
	[Header("Characteristics")]
    [Tooltip("Mass [kg]")]
    public float mass; // Mass of this object. kg	
    [Tooltip("Current velocity [m/s]")]
    public Vector3 velocity; // The average velocity this fixed update. m/s

	public float speed { get { return velocity.magnitude; }}

    public ForceContainer forceContainer = new ForceContainer(); // All the forces currently applied to this object.

	private PhysicsObject[] physicsObjects;

	void Start()
    {
		SetupThrustTrailRenderer();
		if (GetComponent<RocketEngine>())
		{
			mass += GetComponent<RocketEngine>().fuelMass; 
		}

		physicsObjects = FindObjectsOfType<PhysicsObject>();
    }

    void FixedUpdate()
    {
		CalculateGravity();
        UpdateVelocity();
		
        UpdateThrustTrailRenderer();

        Vector3 deltaS = velocity * Time.deltaTime;
        transform.position += deltaS;

        forceContainer.ClearForces();
    }

    void CalculateGravity()
    {
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

    private void UpdateVelocity()
    {
		Debug.Log("Current Net force of " + name + ": " + forceContainer.NetForce);
        Vector3 acceleration = (forceContainer.NetForce / mass);
        velocity += (acceleration * Time.deltaTime);
    }

    public bool showTrails = true;
	private LineRenderer lineRenderer;
	
	// Use this for initialization
	void SetupThrustTrailRenderer () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

		lineRenderer.startColor = Color.yellow;
		lineRenderer.endColor = Color.yellow;

		lineRenderer.startWidth = 0.2F;
		lineRenderer.endWidth = 0.2F;
		lineRenderer.useWorldSpace = false;
	}
	
	// Update is called once per frame
	void UpdateThrustTrailRenderer () {
		if (showTrails) {
			lineRenderer.enabled = true;

			lineRenderer.positionCount = forceContainer.NumOfForces() * 2;
			int i = 0;
			foreach (Vector3 forceVector in forceContainer.Forces) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i += 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}
}
