using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LabGenerator : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public GameObject[] roomPrefabs;
    public GameObject[] corridorPrefabs;
    public GameObject lockedDoorPrefab;
    public GameObject keyPrefab;
    public Tilemap floorTilemap;

    private int[,] grid;
    private List<Vector2Int> roomPositions;
    private List<Vector2Int> corridorPositions;

    private void Start()
    {
        grid = new int[rows, columns];
        roomPositions = new List<Vector2Int>();
        corridorPositions = new List<Vector2Int>();

        // Place initial room
        Vector2Int startingPosition = new Vector2Int(Random.Range(0, rows), Random.Range(0, columns));
        grid[startingPosition.x, startingPosition.y] = 1;
        roomPositions.Add(startingPosition);

        // Create rooms
        for (int i = 0; i < roomPrefabs.Length; i++)
        {
            int count = Random.Range(1, 4);
            for (int j = 0; j < count; j++)
            {
                CreateRoom();
            }
        }

        // Create corridors
        for (int i = 0; i < corridorPrefabs.Length; i++)
        {
            int count = Random.Range(2, 5);
            for (int j = 0; j < count; j++)
            {
                //CreateCorridor();
            }
        }

        // Place locked door and key
        Vector2Int doorPosition = corridorPositions[Random.Range(0, corridorPositions.Count)];
        Instantiate(lockedDoorPrefab, new Vector3(doorPosition.x + 0.5f, doorPosition.y + 0.5f), Quaternion.identity);
        corridorPositions.Remove(doorPosition);
        Vector2Int keyPosition = roomPositions[Random.Range(0, roomPositions.Count)];
        Instantiate(keyPrefab, new Vector3(keyPosition.x + 0.5f, keyPosition.y + 0.5f), Quaternion.identity);

        // Create floor tiles
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (grid[x, y] == 1)
                {
                    Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity);
                }
                else if (grid[x, y] == 2)
                {
                    Instantiate(corridorPrefabs[Random.Range(0, corridorPrefabs.Length)], new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity);
                }
                floorTilemap.SetTile(new Vector3Int(x, y, 0), floorTilemap.tileAssets[Random.Range(0, floorTilemap.tileAssets.Length)]);
            }
        }
    }

    private void CreateRoom()
    {
        Vector2Int position;
        bool validPosition = false;
        while (!validPosition)
        {
            position = roomPositions[Random.Range(0, roomPositions.Count)];
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0: // Up
                    if (position.y + 3 < columns && grid[position.x, position.y + 1] == 0 && grid[position.x, position.y + 2] == 0 && grid[position.x, position.y + 3] == 0)
                    {
                        grid[position.x, position.y + 1] = 1;
                        grid[position.x, position.y + 2] = 1;
                        grid[position.x, position.y + 3] = 1;
                        roomPositions.Remove(position);
                        validPosition = true;
                        GameObject roomObject = Instantiate(roomPrefab, new Vector3(position.x, 0, position.y + 1.5f), Quaternion.identity);
                        Room room = roomObject.GetComponent<Room>();
                        room.xSize = 1;
                        room.zSize = 3;
                        room.Init();
                    }
                    break;
                case 1: // Right
                    if (position.x + 3 < rows && grid[position.x + 1, position.y] == 0 && grid[position.x + 2, position.y] == 0 && grid[position.x + 3, position.y] == 0)
                    {
                        grid[position.x + 1, position.y] = 1;
                        grid[position.x + 2, position.y] = 1;
                        grid[position.x + 3, position.y] = 1;
                        roomPositions.Remove(position);
                        validPosition = true;
                        GameObject roomObject = Instantiate(roomPrefab, new Vector3(position.x + 1.5f, 0, position.y), Quaternion.identity);
                        Room room = roomObject.GetComponent<Room>();
                        room.xSize = 3;
                        room.zSize = 1;
                        room.Init();
                    }
                    break;
                case 2: // Down
                    if (position.y - 3 >= 0 && grid[position.x, position.y - 1] == 0 && grid[position.x, position.y - 2] == 0 && grid[position.x, position.y - 3] == 0)
                    {
                        grid[position.x, position.y - 1] = 1;
                        grid[position.x, position.y - 2] = 1;
                        grid[position.x, position.y - 3] = 1;
                        roomPositions.Remove(position);
                        validPosition = true;
                        GameObject roomObject = Instantiate(roomPrefab, new Vector3(position.x, 0, position.y - 1.5f), Quaternion.identity);
                        Room room = roomObject.GetComponent<Room>();
                        room.xSize = 1;
                        room.zSize = 3;
                        room.Init();
                    }
                    break;
                case 3: // Left
                    if (position.x - 3 >= 0 && grid[position.x - 1, position.y] == 0 && grid[position.x - 2, position.y] == 0 && grid[position.x - 3, position.y] == 0)
                    {
                        grid[position.x - 1, position.y] = 1;
                        grid[position.x - 2, position.y] = 1;
                        grid[position.x - 3, position.y] = 1;
                        roomPositions.Remove(position);
                        validPosition = true;
                        GameObject roomObject = Instantiate(roomPrefab, new Vector3(position.x - 1.5f, 0, position.y), Quaternion.identity);
                        Room room = roomObject.GetComponent<Room>();
                        room.xSize = 3;
                        room.zSize = 1;
                        room.Init();
                    }
                    break;
            }
        }
    }
}
