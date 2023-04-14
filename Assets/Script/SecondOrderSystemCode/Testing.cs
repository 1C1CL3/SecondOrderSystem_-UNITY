using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public float m = 1.0f;  // ����
    public float c = 1.0f;  // ������
    public float k = 1.0f;  // ������ ���
    public float f = 0.0f;  // �ܺ� ��

    private float x = 0.0f;  // ��ġ
    private float v = 0.0f;  // �ӵ�

    public GameObject targetObject;  // Second Order System�� ������ ���� ������Ʈ

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        // ���ӵ� ���
        float a = (f - c * v - k * x) / m;

        // �ӵ� ���
        v += a * dt;

        // ��ġ ���
        x += v * dt;

        transform.position = new Vector3(x, 0.0f, 0.0f);

        // Second Order System�� ������ ���� ������Ʈ�� ��ġ ����
        if (targetObject != null)
        {
            Vector3 targetPos = targetObject.transform.position;
            targetObject.transform.position = new Vector3(targetPos.x + x, targetPos.y, targetPos.z);
        }
    }
}
