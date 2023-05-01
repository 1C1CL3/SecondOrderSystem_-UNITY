using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDy : MonoBehaviour
{
    private SecondOrderState _state;

    public float k1; // damping coefficient
    public float k2; // spring constant
    public float k3; // input gain

    public float f => _state.F;
    public float z => _state.Z;
    public float r => _state.R;

    public float dt; // time step
    private Vector3 velocity, yd;// state variables x velocity of the vector
    private Vector3 position, xd; // y position of the vector
    private Vector3 targetPosition;// prvious input

    private Vector3 previousTargetValue; // prvious input
    private Vector3 targetVelocity;
    private Vector3 currentValue; // prvious input
    private Vector3 currentVelocity;

    public Transform targetTransform;


    void Start()
    {
    }
    public void SecondOrderDynamics()
    {
        //compute constants
        k1 = z / (Mathf.PI * f);
        k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);
        // initialize variables
        previousTargetValue = targetTransform.position;
        currentValue = targetTransform.position;
        currentVelocity = default;
        //yd = Vector3.zero;
    }


    void FixedUpdate()
    {
        SecondOrderDynamics();
        dt = Time.fixedDeltaTime;


        //if (xd == null)// estimate velocity
        //{
        //    xd = (x - targetPosition) / dt;
        //    xd = (x - targetPosition) / dt;
        //    targetPosition = x;
        //}
        //y = y + dt * yd;
        //yd = yd + dt * (x + k3 * xd - y - k1 * yd) / k2;
        //transform.position = y;
        //xp = targetPosition, x = targetTransform.position, xd = targetVelocity, y = currentValue, yd = currentVelocity, 

        if (targetVelocity == null)
        {
            targetVelocity = (targetTransform.position - previousTargetValue) / dt;
            previousTargetValue = targetTransform.position;
        }
        currentValue = currentValue + dt * currentVelocity;
        currentVelocity = currentVelocity + dt * (targetTransform.position + (k3 * targetVelocity) - currentValue - (k1 * currentVelocity)) / k2;

        transform.position = currentValue;


        //transform.position = new Vector3(position.x, position.y, 0f);

        //transform.position = x;
    }
}
