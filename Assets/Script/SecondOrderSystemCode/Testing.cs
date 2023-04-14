using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public float m = 1.0f;  // 질량
    public float c = 1.0f;  // 감쇠계수
    public float k = 1.0f;  // 스프링 상수
    public float f = 0.0f;  // 외부 힘

    private float x = 0.0f;  // 위치
    private float v = 0.0f;  // 속도

    public GameObject targetObject;  // Second Order System의 영향을 받을 오브젝트

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        // 가속도 계산
        float a = (f - c * v - k * x) / m;

        // 속도 계산
        v += a * dt;

        // 위치 계산
        x += v * dt;

        transform.position = new Vector3(x, 0.0f, 0.0f);

        // Second Order System의 영향을 받을 오브젝트의 위치 변경
        if (targetObject != null)
        {
            Vector3 targetPos = targetObject.transform.position;
            targetObject.transform.position = new Vector3(targetPos.x + x, targetPos.y, targetPos.z);
        }
    }
}
