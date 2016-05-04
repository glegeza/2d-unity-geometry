using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class CircleGenerator : MonoBehaviour
{
    public int Points;

    private Mesh _mesh;
    private Vector3[] _verts;
    private int[] _triangles;
    private MeshFilter _meshFilter;

    void Start()
    {
        RebuildMesh();
    }

    private void OnValidate()
    {
        RebuildMesh();
    }

    private void RebuildMesh()
    {
        _mesh = new Mesh();

        // Set vertices
        _verts = new Vector3[Points + 1];
        _verts[0] = Vector3.zero;
        var points = GetOrderedPoints();
        for (var i = 1; i < Points + 1; i++)
        {
            _verts[i] = points[i - 1];
        }
        _mesh.vertices = _verts;

        // Set indices
        _triangles = new int[(Points + 1) * 3];
        for (var i = 3; i < Points * 3 + 1; i += 3)
        {
            _triangles[i] = 0;
            var curPoint = i / 3;
            _triangles[i + 2] = curPoint;
            _triangles[i + 1] = curPoint != Points ? curPoint + 1 : 1;
        }
        _mesh.triangles = _triangles;

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }

    private List<Vector3> GetOrderedPoints()
    {
        var points = new List<Vector3>();
        var increment = Mathf.PI * 2 / Points;
        for (var i = 0; i < Points; i++)
        {
            var angle = i * increment;
            points.Add(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f));
        }
        return points;
    }
}
