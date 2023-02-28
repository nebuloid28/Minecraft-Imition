using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : Ragu
// 최초작성일 : 2023-02-27

public class Chunk : MonoBehaviour
{
    int vertexIndex = 0;

    bool[,,] voxelMap = new bool[VoxelData.chunkWidth, VoxelData.chunkHeigth, VoxelData.chunkWidth];

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uv = new List<Vector2>();

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    void Start()
    {
        FillVoxelMap();
        CreateMeshData();
        DrawMesh();
    }

    void FillVoxelMap()
    {
        for (int y = 0; y < VoxelData.chunkHeigth; y++)
        {
            for (int x = 0; x < VoxelData.chunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.chunkWidth; z++)
                {
                    voxelMap[x, y, z] = true;
                }
            }
        }
    }

    void CreateMeshData()
    {
        for (int y = 0; y < VoxelData.chunkHeigth; y++)
        {
            for (int x = 0; x < VoxelData.chunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.chunkWidth; z++)
                {
                    VoxeltoChunk(new Vector3(x, y, z));
                }
            }
        }
    }

    bool CheckVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        if (x < 0 || x > VoxelData.chunkWidth - 1
         || y < 0 || y > VoxelData.chunkHeigth - 1
         || z < 0 || z > VoxelData.chunkWidth - 1) return false;

        return voxelMap[x, y, z];
    }

    void VoxeltoChunk(Vector3 pos) //Vector3 pos를 참조 변수로 선언
    {
        // 6방향의 면 그리기
        for (int i = 0; i < 6; i++)
        {
            if(!CheckVoxel(pos + VoxelData.faceChecks[i])) //pos와 각 방향을 대조함
            {
                for(int j = 0; j < 4; j++)
                {
                    //면마다 4개의 버텍스와 uv좌표를 각각 지정
                    vertices.Add(pos + VoxelData.voxelVertices[VoxelData.voxelTriangles[i, j]]);
                    uv.Add(VoxelData.voxelUv[j]);
                }
                //시계방향으로 두 삼각형을 그림
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex+1);
                triangles.Add(vertexIndex+2);
                triangles.Add(vertexIndex+2);
                triangles.Add(vertexIndex+1);
                triangles.Add(vertexIndex+3);
                vertexIndex += 4;
                /*
                for (int j = 0; j < 6; j++)
                {
                    int triangleIndex = VoxelData.voxelTriangles[i, j];
                    vertices.Add(VoxelData.voxelVertices[triangleIndex] + pos);
                    triangles.Add(vertexIndex);
                    uv.Add(VoxelData.voxelUv[j]);
                    vertexIndex++;
                }
                */
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
