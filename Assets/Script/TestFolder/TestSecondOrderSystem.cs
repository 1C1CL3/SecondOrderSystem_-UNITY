using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestSecondOrderSystem : MonoBehaviour
{
    public float k1; // damping coefficient
    public float k2; // spring constant
    public float k3; // input gain

    public float f;
    public float z;
    public float r;

    public float dt; // time step
    private Vector3 velocity, yd;// state variables x velocity of the vector
    private Vector3 position, xd; // y position of the vector
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
        //velocity = targetTransform.position;
        yd = Vector3.zero;
    }


    void FixedUpdate()
    {
        SecondOrderDynamics();
        dt = Time.fixedDeltaTime;
        //if (xd == null)// estimate velocity
        //{
        //    xd = (x_position - targetPosition) / dt;
        //    xd = (x_position - targetPosition) / dt;
        //    targetPosition = x_position;
        //}
        //y_velocity = y_velocity + dt * yd;
        //yd = yd + dt * (x_position + k3 * xd - y_velocity - k1 * yd) / k2;
        //transform.position = x_position;



        //Vector3 targetPosition = targetTransform.position;
        //Vector3 deltaPosition = targetPosition - position;
        //Vector3 acceleration = deltaPosition / dt - k1 * velocity;

        //Vector3 velocityNext = velocity + acceleration * dt;
        //Vector3 positionNext = position + velocityNext * dt;
        ////yd positionnext
        //velocity = velocityNext + k3 * deltaPosition / k2;// yd = yd + dt * (x+ k3 * xd - y - k1 * yd) / k2
        //position = positionNext;


        //transform.position = new Vector3(position.x, position.y, 0f);
        Vector3 targetPosition = targetTransform.position;
        Vector3 deltaPosition = targetPosition - position;
        Vector3 acceleration = deltaPosition / dt - velocity * k1;

        Vector3 velocityNext = velocity + acceleration * dt;
        Vector3 positionNext = position + velocityNext * dt;

        velocity = velocityNext + deltaPosition * k3 / k2;
        position = positionNext;

        transform.position = new Vector3(position.x, position.y, 0f);

        //transform.position = position;
        

    }





}
