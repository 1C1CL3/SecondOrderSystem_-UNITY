using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public GameObject target;        // 타겟 오브젝트
    public GameObject affectedObject;   // 영향을 받는 오브젝트

    public float k1 = 1f;   // 가속도를 계산하기 위한 상수
    public float k2 = 1f;   // 가속도를 계산하기 위한 상수
    public float k3 = 1f;   // 가속도를 계산하기 위한 상수

    public float T = 0.1f;  // 시간 간격

    private void Update()
    {
        // 타겟 오브젝트의 위치를 가져옴
        Vector3 targetPos = target.transform.position;

        // 위치 계산
        float y = affectedObject.transform.position.y;
        float yDot = affectedObject.GetComponent<Rigidbody>().velocity.y;
        y = y + T * yDot;

        // 속도 계산
        float x = (targetPos.y - affectedObject.transform.position.y) / T;
        float xDot = (x - affectedObject.GetComponent<Rigidbody>().velocity.y) / T;
        yDot = yDot + T * (x + k3 * xDot - y - k1 * yDot) / k2;

        // 가속도 계산
        float a = (x + k3 * xDot - y - k1 * yDot) / k2;

        // 영향을 받는 오브젝트에 가속도 적용
        affectedObject.GetComponent<Rigidbody>().AddForce(Vector3.up * a);

    }
}
