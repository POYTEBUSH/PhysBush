using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;

    public Vector2 speed = new Vector2(250.0f, 120.0f);

    public float yminLimit = -20.0f;
    public float ymaxLimit = 80.0f;
    
    private float x = 0.0f;
    private float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotations.
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true; 
        }
    }

    void LateUpdate () 
    {
    if (target) {
        x += Input.GetAxis("Mouse X") * speed.x * 0.02f;
        y -= Input.GetAxis("Mouse Y") * speed.y * 0.02f;
        distance -= Input.mouseScrollDelta.y * 0.2f; // Line added
 		
 		y = ClampAngle(y, yminLimit, ymaxLimit);
 		       
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        
        transform.rotation = rotation;
        transform.position = position;
    }
}

    static float ClampAngle(float angle, float min, float max) 
    {
	    if (angle < -360)
		    angle += 360;
	    if (angle > 360)
		    angle -= 360;
	    return Mathf.Clamp (angle, min, max);
    }
}
