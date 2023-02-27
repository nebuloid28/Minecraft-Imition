using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : Ragu
// 최초작성일 : 2023-02-27

public class Chunk : MonoBehaviour
{
    int vertexIndex = 0;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uv = new List<Vector2>();

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    void Start()
    {
        VoxeltoChunk();
        DrawMesh();
    }

    void VoxeltoChunk()
    {
        // 6방향의 면 그리기
        for (int p = 0; p < 6; p++)
        {
            // 각 면의 삼각형 2개 그리기
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = VoxelData.voxelTriangles[p, i];

                vertices.Add(VoxelData.voxelVertices[triangleIndex]);
                triangles.Add(vertexIndex);
                uv.Add(VoxelData.voxelUv[i]);

                vertexIndex++;
            }
        }
    }

    void DrawMesh()
    {
        // 메시에 데이터들 초기화
        Mesh mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            uv = uv.ToArray()
        };
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
