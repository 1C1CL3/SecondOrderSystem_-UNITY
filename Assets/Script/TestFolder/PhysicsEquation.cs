using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.Profiling;

[CustomEditor(typeof(PhysicsEquationComponent))] 
public class PhysicsEquationEditor : Editor
{
    private const int SampleCount = 300; // �׷����� �ػ󵵸� �����մϴ�. �� ���� ���� �� ���� ���� �����մϴ�.
    private const float GraphWidth = 300; // �׷����� ���� ũ���Դϴ�.
    private const float GraphHeight = 300; // �׷����� ���� ũ���Դϴ�.
    private float[] samples;

    public int xOffset = 100;
    public int yOffset = 350;

    // ���� ���� �����ϱ� ���� ����
    private float prevF, prevZ, prevR;
    //�ùķ��̼��� ���� �ڵ�
    public Vector3 targetNew, targetOrg, v;

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        // ������Ʈ�� ���� ������ ����ϴ�.
        PhysicsEquationComponent component = (PhysicsEquationComponent)target;

        // ���� �����͸� ������ �迭�� �غ��մϴ�.
         samples = new float[SampleCount];

        // ���� �����Ͱ� ���� ��쿡�� ����
        if (samples == null || component.f != prevF || component.z != prevZ || component.r != prevR)
        {
            // �� �ʱ�ȭ
            samples = new float[SampleCount];
            targetNew = new Vector3(1, 1, 1);
            targetOrg = new Vector3(0, 0, 0);
            v = new Vector3(0, 0, 0);

            // �� ������ ���������� ����մϴ�.
            for (int i = 0; i < SampleCount; i++)
            {
                Values sample = component.EvaluateFunction(targetNew, targetOrg, v);// �������� ����Ͽ� ���ÿ� �����մϴ�.
                targetOrg = sample.position;
                v = sample.velocity;
                samples[i] = sample.position.x;
                component.SetSamples(i, samples[i]);
            }

            // ���� ���� ���� ������ ����
            prevF = component.f;
            prevZ = component.z;
            prevR = component.r;
        }

        // �׷����� �׸� ������ �����մϴ�.
        Rect rect = GUILayoutUtility.GetRect(GraphWidth, GraphHeight);

        // GUI �������� �����մϴ�.
        Handles.BeginGUI();
        Handles.color = Color.red; // �׷����� ���� �����մϴ�.

        // ���� ���� ������ �׸��ϴ�.
        for (int j = 1; j < SampleCount; j++)
        {
            // �� ���� x, y ��ġ�� ����մϴ�.
            float xPrev = ((j - 1) / (float)(SampleCount - 1)) * GraphWidth + xOffset;
            float xCurr = (j / (float)(SampleCount - 1)) * GraphWidth + xOffset;

            float yPrev = (1 - (component.GetSamples(j-1) * 0.5f + 0.5f)) * GraphHeight + yOffset;
            float yCurr = (1 - (component.GetSamples(j) * 0.5f + 0.5f)) * GraphHeight + yOffset;

            // �� �� ���̿� ������ �׸��ϴ�.
            Handles.DrawLine(new Vector2(xPrev, yPrev), new Vector2(xCurr, yCurr));
        }
        // GUI �������� �����մϴ�.
        Handles.EndGUI();
    }
}