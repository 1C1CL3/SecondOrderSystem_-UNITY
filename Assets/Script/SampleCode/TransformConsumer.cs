using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformConsumer : MonoSingleton<Transform>
{
    float radius = 10f;
    float speed = 100f;
    float error;

    Vector3 axis;
    Vector3 orbit;
    Vector3 displaceOrbit;

    public float damping = 12f;
    public float bounce = 0.9f;
    public float pull = 0.5f;
    public Vector3 oldPosition;

    protected override void Awake()
    {
        // make orbit physics
        orbit = Random.onUnitSphere * radius;
        transform.position = Instance.position + orbit;
        axis = Random.onUnitSphere;
        axis += (Vector3.Dot(Instance.position, orbit) - Vector2.Dot(axis, orbit)) * Vector3.forward / orbit.z - Instance.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 _oldPosition = transform.position;
        // just an orbit physics
        orbit = Quaternion.AngleAxis(speed * Time.fixedDeltaTime, axis) * orbit;

        // Second Order System
        //use abs to dont make error 
        error = Mathf.Abs((transform.position - Instance.transform.position).magnitude - radius) / radius;
        //displace orbit 
        displaceOrbit = Vector3.Lerp(orbit, Vector3.Normalize(transform.position - Instance.transform.position) * radius, pull * error);
        //use RotateAround to make Stable orbit
        transform.RotateAround(Instance.position, axis, (1 - bounce) * speed * Time.fixedDeltaTime);
        
        //physics
        transform.position = Vector3.Lerp(transform.position, Instance.position + displaceOrbit, (1 - bounce) * damping * Time.fixedDeltaTime);
        transform.position = (1 + bounce) * transform.position - bounce * oldPosition;
        oldPosition = _oldPosition;

    }
}
