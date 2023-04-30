using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDy : MonoBehaviour
{

    public float k1; // damping coefficient
    public float k2; // spring constant
    public float k3; // input gain

    public float f;
    public float z;
    public float r;

    public float dt; // time step
    private Vector3 y, yd;// state variables x position of the vector yd is acceleration
    private Vector3 x, xd; // x velocity of the vector
    private Vector3 targetPosition;// prvious input

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
        targetPosition = targetTransform.position;
        y = targetTransform.position;
        //yd = Vector3.zero;
    }


    void FixedUpdate()
    {
        SecondOrderDynamics();
        dt = Time.fixedDeltaTime;


        if (xd == null)// estimate velocity
        {
            xd = (x - targetPosition) / dt;
            xd = (x - targetPosition) / dt;
            targetPosition = x;
        }
        y = y + dt * yd;
        yd = yd + dt * (x+ k3 * xd - y - k1 * yd) / k2;
        transform.position = x;





        //transform.position = new Vector3(position.x, position.y, 0f);

        //transform.position = x;
    }
}
