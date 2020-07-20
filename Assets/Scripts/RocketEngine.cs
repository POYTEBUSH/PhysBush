using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public class RocketEngine : MonoBehaviour
{
    [Header("Starting Characteristics")]
    [Tooltip("Fuel Mass [kg]")]
    public float fuelMass; //kg
    [Tooltip("Max engine thrust [kN]")]
    public float maxThrust; //kN [kg m/s^2]
    [Tooltip("Current thrust percentage")]
    [Range(0.0f, 1.0f)]
    public float thrustPercent;

    public Vector3 thrustUnitVector;

    #region Privates
    private float currentThrust;
    #endregion

    void FixedUpdate()
    {
        float fuelUsedThisUpdate = FuelThisUpdate();
        if (fuelMass > fuelUsedThisUpdate)
        {
            fuelMass -= fuelUsedThisUpdate;
            GetComponent<PhysicsObject>().mass -= fuelUsedThisUpdate;

            ExcertForce();
        }
    }

    float FuelThisUpdate()
    {        
        const float effectiveExhaustVelocity = 4462.0f;
        float exhaustMassFlowrate = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlowrate * Time.deltaTime; // [kg]
    }

    void ExcertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000.0f;

        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust;
        GetComponent<PhysicsObject>().forceContainer.AddForce(thrustVector);
    }
}
