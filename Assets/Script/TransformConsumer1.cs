//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TransformConsumer : MonoSingleton<Transform>
//{
//    float radius = 10f;
//    float speed = 100f;
//    Vector3 axis;
//    Vector3 orbit;

//    public float damping = 5f;


//    protected override void Awake()
//    {
//        orbit = Random.onUnitSphere * radius;
//        transform.position = Instance.position + orbit;
//        axis = Random.onUnitSphere;
//        axis += (Vector3.Dot(Instance.position, orbit) - Vector2.Dot(axis, orbit)) * Vector3.forward / orbit.z - Instance.position;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        orbit = Quaternion.AngleAxis(speed * Time.deltaTime, axis) * orbit;
//        transform.position = Vector3.Lerp(transform.position, Instance.position + orbit, damping * Time.deltaTime);
//    }
//}
