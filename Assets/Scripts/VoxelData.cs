using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : Ragu
// 최초작성일 : 2023-02-27

// 수정일 : 2023-02-28
// faceChecks 정의
// voxelTriangles, voxelUvs 수정(버텍스 6개 -> 4개)

public class VoxelData : MonoBehaviour
{
    public static readonly int chunkWidth = 10;
    public static readonly int chunkHeigth = 100;

    public static readonly Vector3[] voxelVertices = new Vector3[8]
    {
        /*
         *   7──────6
         *  /|     /|
         * 3──────2 |
         * | |    | |
         * | 4────|─5
         * |/     |/
         * 0──────1
         */
        //Front
        new Vector3(0.0f, 0.0f, 0.0f), //0 LB
        new Vector3(1.0f, 0.0f, 0.0f), //1 RB
        new Vector3(1.0f, 1.0f, 0.0f), //2 RT
        new Vector3(0.0f, 1.0f, 0.0f), //3 LT
        //Back
        new Vector3(0.0f, 0.0f, 1.0f), //4 LB
        new Vector3(1.0f, 0.0f, 1.0f), //5 RB
        new Vector3(1.0f, 1.0f, 1.0f), //6 RT
        new Vector3(0.0f, 1.0f, 1.0f), //7 LT
    };

    public static readonly Vector3[] faceChecks = new Vector3[6]
    {
        new Vector3( 0.0f,  0.0f, -1.0f), //Back Face
        new Vector3( 0.0f,  0.0f, +1.0f), //Front Face
        new Vector3( 0.0f, +1.0f,  0.0f), //Top Face
        new Vector3( 0.0f, -1.0f,  0.0f), //Bottom Face
        new Vector3(-1.0f,  0.0f,  0.0f), //Left Face
        new Vector3(+1.0f,  0.0f,  0.0f), //RIght Face
    };

    public static readonly int[,] voxelTriangles = new int[6, 4]
    {
        //버텍스의 순서는 LB-LT-RB -- RB-LT-RT
        //버텍스 인덱스가 시계방향인 이유 : 외적의 방향이 화면방향으로 향하게됨

        {0, 3, 1, 2}, //Back Face
        {5, 6, 4, 7}, //Front Face
        {3, 7, 2, 6}, //Top Face
        {1, 5, 0, 4}, //Bottom Face
        {4, 7, 0, 3}, //Left Face
        {1, 2, 5, 6}, //RIght Face
    };

    public static readonly Vector2[] voxelUv = new Vector2[4]
    {
        /*
         * voxelTris의 버텍스 인덱스 순서에 따른 UV좌표 데이터
         * 
         * LB-LT-RB  -- RB-LT-RT
         *   3            3ㅡ2
         *   |＼           ＼|
         *   0ㅡ1            1
         */
        new Vector2(0.0f, 0.0f), //LB
        new Vector2(0.0f, 1.0f), //LT
        new Vector2(1.0f, 0.0f), //RB
        //new Vector2(1.0f, 0.0f), //RB
        //new Vector2(0.0f, 1.0f), //LT
        new Vector2(1.0f, 1.0f), //RT
    };
}
