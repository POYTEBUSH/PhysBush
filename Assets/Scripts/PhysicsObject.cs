using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float mass; // Mass of this object.

    public Vector3 velocity; // The average velocity this fixed update.

    public ForceContainer forceContainer = new ForceContainer(); // All the forces currently applied to this object.

    void FixedUpdate()
    {
        UpdateVelocity();

        Vector3 deltaS = velocity * Time.deltaTime;
        transform.position += deltaS;

        forceContainer.ClearForces();
    }

    private void UpdateVelocity()
    {
        Vector3 acceleration = (forceContainer.NetForce / mass);
        velocity += (acceleration * Time.deltaTime);
    }

    public bool showTrails = true;
	private LineRenderer lineRenderer;
	
	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

		lineRenderer.startColor = Color.yellow;
		lineRenderer.endColor = Color.yellow;

		lineRenderer.startWidth = 0.2F;
		lineRenderer.endWidth = 0.2F;
		lineRenderer.useWorldSpace = false;
	}
	
	// Update is called once per frame
	void Update () {
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
