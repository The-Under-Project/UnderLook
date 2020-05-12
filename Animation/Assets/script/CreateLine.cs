using UnityEngine;
using System.Collections;
using System.Drawing;

[RequireComponent(typeof(LineRenderer))]
public class CreateLine : MonoBehaviour
{
    // code found here : https://answers.unity.com/questions/1226025/how-to-render-a-linerenderer-through-multiple-poin.html
    LineRenderer lineRenderer;
    private Vector3[] vP;
    int seg = 20;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }
    public void Line(Transform[] points)
    {
        
        seg = points.Length;
        vP = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            vP[i] = points[i].position;
        }
        for (int i = 0; i < seg; i++)
        {
            float t = i / (float)seg;
            lineRenderer.SetVertexCount(seg);
            lineRenderer.SetPositions(vP);
        }

    }
}