using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public struct Values
{
    public Vector3 position;
    public Vector3 velocity;
}

public class PhysicsEquationComponent : MonoBehaviour
{

    public float k1; // damping coefficient
    public float k2; // spring constant
    public float k3; // input gain
    [Range(0, 20)]
    public float f;
    [Range(0, 20)]
    public float z = 1;
    [Range(0, 1)]
    public float r=0;

    public float gPos;

    public float dt; // time step
    public Vector3 velocity, yd;// state variables x velocity of the vector
    private Vector3 position, xd; // y position of the vector
    public Vector3 fakePosition2; // y position of the vector
    public Vector3 fakePosition = new Vector3(1, 1, 1); // y position of the vector
    private Vector3 targetPosition;// prvious input

    public Transform targetTransform;

    public float[] samples = new float[300];


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

    public void SetSamples(int i,float s)
    {
        samples[i]=s;
    }

    public float GetSamples(int i)
    {
        return samples[i];
    }

    public Values EvaluateFunction(Vector3 x, Vector3 y, Vector3 v)
    {
        SecondOrderDynamics();
        Values d;
        Vector3 targetPosition = x;
        Vector3 deltaPosition = targetPosition - y;
        Vector3 acceleration = deltaPosition / 0.01f - v * k1;

        Vector3 velocityNext = v + acceleration * 0.01f;
        Vector3 positionNext = y + velocityNext * 0.01f;

        v = velocityNext + deltaPosition * k3 / k2;
        y = positionNext;
        d.position = y;
        d.velocity = v;
        return d;
    }

    void FixedUpdate()
    {
        //if (xd == null)// estimate velocity
        //{
        //    xd = (x_position - targetPosition) / dt;
        //    xd = (x_position - targetPosition) / dt;
        //    targetPosition = x_position;
        //}
        //y_velocity = y_velocity + dt * yd;
        //yd = yd + dt * (x_position + k3 * xd - y_velocity - k1 * yd) / k2;
        //transform.position = x_position;
        SecondOrderDynamics();
        dt = Time.fixedDeltaTime;
        Vector3 targetPosition = targetTransform.position;
        Vector3 deltaPosition = targetPosition - position;
        Vector3 acceleration = deltaPosition / dt - velocity * k1;

        Vector3 velocityNext = velocity + acceleration * dt;
        Vector3 positionNext = position + velocityNext * dt;

        velocity = velocityNext + deltaPosition * k3 / k2;
        position = positionNext;

        transform.position = position;

    }

}