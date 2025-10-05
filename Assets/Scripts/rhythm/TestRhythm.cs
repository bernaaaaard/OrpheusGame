using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TestRhythm : MonoBehaviour
{
    [Header("Beat Settings")]
    public float bpm = 120f;
    private float beatInterval;
    private float beatTimer;

    [Header("Circle Settings")]
    public int segments = 100;        // smoothness of the circle
    public float innerRadius = 1.5f;  // stationary inner circle radius
    public float outerBaseRadius = 3f;// max radius of outer circle
    public float lineWidth = 0.05f;

    private LineRenderer outerCircle;
    private LineRenderer innerCircle;

    private void Start()
    {
        beatInterval = 60f / bpm;
        beatTimer = 0f;

        // Create two GameObjects with LineRenderers
        GameObject outerObj = new GameObject("OuterCircle");
        outerObj.transform.parent = transform;
        outerCircle = outerObj.AddComponent<LineRenderer>();

        GameObject innerObj = new GameObject("InnerCircle");
        innerObj.transform.parent = transform;
        innerCircle = innerObj.AddComponent<LineRenderer>();

        SetupLineRenderer(outerCircle);
        SetupLineRenderer(innerCircle);

        // Draw the inner circle once (it never moves)
        DrawCircle(innerCircle, innerRadius);
    }

    private void SetupLineRenderer(LineRenderer lr)
    {
        lr.positionCount = segments + 1;
        lr.loop = true;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.useWorldSpace = false;
    }

    private void Update()
    {
        beatTimer += Time.deltaTime;
        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
        }

        // normalized 0-1 value across each beat
        float t = beatTimer / beatInterval;
        float pulse = Mathf.Sin(t * Mathf.PI);

        // Outer circle shrinks down to inner circle, then expands back out
        float outerRadius = Mathf.Lerp(outerBaseRadius, innerRadius, pulse);

        DrawCircle(outerCircle, outerRadius);
    }

    private void DrawCircle(LineRenderer lr, float radius)
    {
        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

    //Editor-only preview
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        DrawGizmoCircle(innerRadius);

        Gizmos.color = Color.yellow;
        DrawGizmoCircle(outerBaseRadius);
    }

    private void DrawGizmoCircle(float radius)
    {
        Vector3 prevPoint = transform.position + new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * radius;
        for (int i = 1; i <= segments; i++)
        {
            float angle = (i / (float)segments) * Mathf.PI * 2f;
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}