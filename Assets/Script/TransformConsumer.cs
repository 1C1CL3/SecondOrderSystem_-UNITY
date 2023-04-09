using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformConsumer : MonoSingleton<Transform>
{
    float radius = 10f;
    float speed = 100f;
    Vector3 axis;
    Vector3 orbit;

    public float damping = 12f;
    public float bounce = 0.9f;
    public Vector3 oldPosition;

    protected override void Awake()
    {
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
        orbit = Quaternion.AngleAxis(speed * Time.deltaTime, axis) * orbit;

        // Second Order System
        transform.position = Vector3.Lerp(transform.position, Instance.position + orbit, (1 - bounce) * damping * Time.deltaTime);
        transform.position = (1 + bounce) * transform.position - bounce * oldPosition;
        oldPosition = _oldPosition;

    }
}
