using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private float BSPTimeElapsed;
    [SerializeField] private int mapWidth, mapLength;
    [SerializeField] private int roomWidthMin, roomLengthMin;
    [SerializeField] private int maxIterations;
    [SerializeField] private int corridorWidth;
    [SerializeField] private Material material;
    [Range(0.0f, 0.3f)]
    [SerializeField] private float roomBottomCornerModifier;
    [Range(0.7f, 1.0f)]
    [SerializeField] private float roomTopCornerMidifier;
    [Range(0, 2)]
    [SerializeField] private int roomOffset;

    public void BFS()
    {
        PathFindingBFS.BFSForSSSP(MapGenerator.allNodesCollection[0]);
    }

    [SerializeField] private GameObject wallVertical, wallHorizontal;

    private List<Vector3Int> possibleDoorVerticalPosition;
    private List<Vector3Int> possibleDoorHorizontalPosition;
    private List<Vector3Int> possibleWallHorizontalPosition;
    private List<Vector3Int> possibleWallVerticalPosition;

    public static float StartTime;
    public  float startTime, endTime;

    void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        startTime = DateTime.Now.Millisecond;
        StartTime = startTime;
        DestroyAllChildren();

        MapGenerator generator = new MapGenerator(mapWidth, mapLength);
        List<BSPNode> listOfRooms = generator.CalculateMap(maxIterations,
            roomWidthMin,
            roomLengthMin,
            roomBottomCornerModifier,
            roomTopCornerMidifier,
            roomOffset,
            corridorWidth);
        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;
        possibleDoorVerticalPosition = new List<Vector3Int>();
        possibleDoorHorizontalPosition = new List<Vector3Int>();
        possibleWallHorizontalPosition = new List<Vector3Int>();
        possibleWallVerticalPosition = new List<Vector3Int>();


        for (int i = 0; i < listOfRooms.Count; i++)
        {
            RenderMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);
        }

        if (wallVertical != null && wallHorizontal != null)
        {
            CreateWalls(wallParent);

        }



        endTime = DateTime.Now.Millisecond - startTime;
        BSPTimeElapsed = endTime;
       // Debug.Log(BSPTimeElapsed);

    }

    private void CreateWalls(GameObject wallParent)
    {
        foreach (var wallPosition in possibleWallHorizontalPosition)
        {
            CreateWall(wallParent, wallPosition, wallHorizontal);
        }
        foreach (var wallPosition in possibleWallVerticalPosition)
        {
            CreateWall(wallParent, wallPosition, wallVertical);
        }
    }

    private void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject wallPrefab)
    {
        wallPrefab.name = $"{wallPrefab.name}"; //_child_of_{wallParent.name}
        Instantiate(wallPrefab, wallPosition, Quaternion.identity, wallParent.transform);
    }

    private void RenderMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeftV = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightV = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftV = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightV = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            bottomLeftV,
            bottomRightV
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3
        };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GameObject mapFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));
        mapFloor.transform.position = Vector3.zero;
        mapFloor.transform.localScale = Vector3.one;
        mapFloor.GetComponent<MeshFilter>().mesh = mesh;
        mapFloor.GetComponent<MeshRenderer>().material = material;
        mapFloor.transform.parent = transform;

        if (wallVertical != null && wallHorizontal != null)
        {
            for (int row = (int)bottomLeftV.x; row < (int)bottomRightV.x; row++)
            {
                var wallPosition = new Vector3(row, 2, bottomLeftV.z);
                AddWallPositionToList(wallPosition, possibleWallHorizontalPosition, possibleDoorHorizontalPosition);
            }
            for (int row = (int)topLeftV.x; row < (int)topRightCorner.x; row++)
            {
                var wallPosition = new Vector3(row, 2, topRightV.z);
                AddWallPositionToList(wallPosition, possibleWallHorizontalPosition, possibleDoorHorizontalPosition);
            }
            for (int col = (int)bottomLeftV.z; col < (int)topLeftV.z; col++)
            {
                var wallPosition = new Vector3(bottomLeftV.x, 2, col);
                AddWallPositionToList(wallPosition, possibleWallVerticalPosition, possibleDoorVerticalPosition);
            }
            for (int col = (int)bottomRightV.z; col < (int)topRightV.z; col++)
            {
                var wallPosition = new Vector3(bottomRightV.x, 2, col);
                AddWallPositionToList(wallPosition, possibleWallVerticalPosition, possibleDoorVerticalPosition);
            }
        }
    }

    private void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList, List<Vector3Int> doorList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPosition);
        if (wallList.Contains(point))
        {
            doorList.Add(point);
            wallList.Remove(point);
        }
        else
        {
            wallList.Add(point);
        }
    }

    public void DestroyAllChildren()
    {
        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }

        }

        while (possibleDoorVerticalPosition.Count > 0 && possibleDoorVerticalPosition != null)
        {
            possibleDoorVerticalPosition.Clear();

        }
        while (possibleDoorHorizontalPosition.Count > 0 && possibleDoorHorizontalPosition != null)
        {
            possibleDoorHorizontalPosition.Clear();

        }
        while (possibleWallHorizontalPosition.Count > 0 && possibleWallHorizontalPosition != null)
        {
            possibleWallHorizontalPosition.Clear();

        }
        while (possibleWallVerticalPosition.Count > 0 && possibleWallVerticalPosition != null)
        {
            possibleWallVerticalPosition.Clear();

        }

    }
}
