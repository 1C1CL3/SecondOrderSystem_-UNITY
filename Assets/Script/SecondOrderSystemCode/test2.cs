using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public GameObject target;        // Ÿ�� ������Ʈ
    public GameObject affectedObject;   // ������ �޴� ������Ʈ

    public float k1 = 1f;   // ���ӵ��� ����ϱ� ���� ���
    public float k2 = 1f;   // ���ӵ��� ����ϱ� ���� ���
    public float k3 = 1f;   // ���ӵ��� ����ϱ� ���� ���

    public float T = 0.1f;  // �ð� ����

    private void Update()
    {
        // Ÿ�� ������Ʈ�� ��ġ�� ������
        Vector3 targetPos = target.transform.position;

        // ��ġ ���
        float y = affectedObject.transform.position.y;
        float yDot = affectedObject.GetComponent<Rigidbody>().velocity.y;
        y = y + T * yDot;

        // �ӵ� ���
        float x = (targetPos.y - affectedObject.transform.position.y) / T;
        float xDot = (x - affectedObject.GetComponent<Rigidbody>().velocity.y) / T;
        yDot = yDot + T * (x + k3 * xDot - y - k1 * yDot) / k2;

        // ���ӵ� ���
        float a = (x + k3 * xDot - y - k1 * yDot) / k2;

        // ������ �޴� ������Ʈ�� ���ӵ� ����
        affectedObject.GetComponent<Rigidbody>().AddForce(Vector3.up * a);

    }
}
