using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBTorque : MonoBehaviour
{
    private Rigidbody rigidBody;

    public Vector3 torque;
    public float torqueTime;
    public float elapsedTime;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //StartCoroutine(AddTorqueRepeating(torqueTime, Time.deltaTime));
    }

    private void FixedUpdate()
    {
        if (elapsedTime < torqueTime)
        {
            elapsedTime += Time.deltaTime;
            rigidBody.AddTorque(torque);
        }
    }

    IEnumerator AddTorqueRepeating(float duration, float waitTime)
    {
        elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(waitTime);
            elapsedTime += Time.deltaTime;

            rigidBody.AddTorque(torque);
        }
    }
}
