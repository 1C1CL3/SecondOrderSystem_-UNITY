using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.Profiling;

[CustomEditor(typeof(PhysicsEquationComponent))] 
public class PhysicsEquationEditor : Editor
{
    private const int SampleCount = 300; // 그래프의 해상도를 결정합니다. 더 높은 값은 더 많은 점을 생성합니다.
    private const float GraphWidth = 300; // 그래프의 가로 크기입니다.
    private const float GraphHeight = 300; // 그래프의 세로 크기입니다.
    private float[] samples;

    public int xOffset = 100;
    public int yOffset = 350;

    // 이전 값을 저장하기 위한 변수
    private float prevF, prevZ, prevR;
    //시뮬레이션을 위한 코드
    public Vector3 targetNew, targetOrg, v;

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        // 컴포넌트에 대한 참조를 얻습니다.
        PhysicsEquationComponent component = (PhysicsEquationComponent)target;

        // 샘플 데이터를 저장할 배열을 준비합니다.
         samples = new float[SampleCount];

        // 샘플 데이터가 없을 경우에만 생성
        if (samples == null || component.f != prevF || component.z != prevZ || component.r != prevR)
        {
            // 값 초기화
            samples = new float[SampleCount];
            targetNew = new Vector3(1, 1, 1);
            targetOrg = new Vector3(0, 0, 0);
            v = new Vector3(0, 0, 0);

            // 각 샘플을 방정식으로 계산합니다.
            for (int i = 0; i < SampleCount; i++)
            {
                Values sample = component.EvaluateFunction(targetNew, targetOrg, v);// 방정식을 계산하여 샘플에 저장합니다.
                targetOrg = sample.position;
                v = sample.velocity;
                samples[i] = sample.position.x;
                component.SetSamples(i, samples[i]);
            }

            // 현재 값을 이전 값으로 저장
            prevF = component.f;
            prevZ = component.z;
            prevR = component.r;
        }

        // 그래프를 그릴 영역을 지정합니다.
        Rect rect = GUILayoutUtility.GetRect(GraphWidth, GraphHeight);

        // GUI 렌더링을 시작합니다.
        Handles.BeginGUI();
        Handles.color = Color.red; // 그래프의 색을 지정합니다.

        // 샘플 간에 라인을 그립니다.
        for (int j = 1; j < SampleCount; j++)
        {
            // 각 점의 x, y 위치를 계산합니다.
            float xPrev = ((j - 1) / (float)(SampleCount - 1)) * GraphWidth + xOffset;
            float xCurr = (j / (float)(SampleCount - 1)) * GraphWidth + xOffset;

            float yPrev = (1 - (component.GetSamples(j-1) * 0.5f + 0.5f)) * GraphHeight + yOffset;
            float yCurr = (1 - (component.GetSamples(j) * 0.5f + 0.5f)) * GraphHeight + yOffset;

            // 두 점 사이에 라인을 그립니다.
            Handles.DrawLine(new Vector2(xPrev, yPrev), new Vector2(xCurr, yCurr));
        }
        // GUI 렌더링을 종료합니다.
        Handles.EndGUI();
    }
}