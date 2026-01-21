using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryVisualizer : MonoBehaviour {
    public int maxReflections = 5;
    public float maxDistance = 50f;
    public LayerMask collisionLayers;
    public string targetTag = "PuzzleTarget";

    [Header("Feedback colros")]
    public Color colorMiss = Color.white;
    public Color colorBouncing = Color.red;
    public Color colorSuccess = Color.blue;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;

        if (lineRenderer.startWidth < 0.01f) {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
        }
    }

    public void DrawPath(Vector3 origin, Vector3 direction)
    {
        lineRenderer.enabled = true;

        List<Vector3> points = new List<Vector3>();
        points.Add(origin);

        Vector3 currentPos = origin;
        Vector3 currentDir = direction;
        float remainingDistance = maxDistance;
        
        Color finalColor = colorMiss; 
        bool hitWall = false;

        for (int i = 0; i <= maxReflections; i++) {
            Ray ray = new Ray(currentPos, currentDir);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit, remainingDistance, collisionLayers)) {
                points.Add(hit.point);
                remainingDistance -= Vector3.Distance(currentPos, hit.point);

                if (hit.collider.CompareTag(targetTag)) {
                    finalColor = colorSuccess;
                    break; 
                } else {
                    hitWall = true;
                    finalColor = colorBouncing;
                }

                currentDir = Vector3.Reflect(currentDir, hit.normal);
                currentPos = hit.point + (currentDir * 0.01f);
            } else {
                points.Add(currentPos + (currentDir * remainingDistance));
                break;
            }
        }

        SetLineColor(finalColor);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void HidePath()
    {
        if(lineRenderer != null) lineRenderer.enabled = false;
    }

    private void SetLineColor(Color color)
    {
        if (lineRenderer.material != null) lineRenderer.material.color = color;
            
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}