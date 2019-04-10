using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public MazeCell cellPrefab;
    public int maxRoomSize = 15;
    public int obstaclesToComplete = 2;
    public IntVector2 size;
    public float generationStepDelay;
    public MazePassage passagePrefab;
    public int mazeWallMultiplier;
    public MazeWall[] wallPrefabs;
    public MazeDoor doorPrefab;
    [Range(0f, 1f)]
    public float doorProbability;
    public MazeRoomSettings[] roomSettings;
    public GameObject[] mazeObstacles;
    public FloatVariable obstaclesCompleted;
    public FloatVariable obstaclesRemaining;
    public RoomVariable currentRoom;
    public GameObject portalPrefab;
    public Transform playerTransform;
    public Transform playerCameraTransform;

    private MazeCell[,] cells;

    private List<MazeRoom> rooms = new List<MazeRoom>();

    private MazeRoom CreateRoom(int indexToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
        if (newRoom.settingsIndex == indexToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public void SpawnEndGame()
    {
        if (ObjectiveIsCompleted())
        {
            MazeCell cell = currentRoom.RuntimeValue.RandomUnfilledCell();
            GameObject portal = Instantiate(portalPrefab, cell.transform);
            portal.transform.localPosition = Vector3.zero;

            Debug.Log("Beginning to assign portal Teleporter transforms");
            PortalTeleporter[] teleporters = portal.GetComponentsInChildren<PortalTeleporter>();
            foreach (PortalTeleporter t in teleporters) {
                t.player = playerTransform;
                Debug.Log("assigning portal teleporter player transform");
                //t.player = playerCameraTransform;
            }
            Debug.Log("Beginning to assign portal Camera transforms");
            PortalCamera[] cameras = portal.GetComponentsInChildren<PortalCamera>();
            foreach (PortalCamera c in cameras)
            {
                Debug.Log("assigning portal camera player camera transform");
                c.playerCamera = playerCameraTransform;
            }

            Debug.Log("Finished to assigning all transforms");
        }
    }

    public void CheckObstaclesRemaining()
    {
        obstaclesRemaining.RuntimeValue = Mathf.Min(obstaclesToComplete, rooms.Count) - obstaclesCompleted.RuntimeValue;
    }

    public bool ObjectiveIsCompleted()
    {
        return obstaclesCompleted.RuntimeValue >= Mathf.Min(obstaclesToComplete, rooms.Count);
    }

    public void UnlockRoom()
    {
        currentRoom.RuntimeValue.Unlock();
    }

    //public IEnumerator Generate()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
    //    cells = new MazeCell[size.x, size.z];
    //    List<MazeCell> activeCells = new List<MazeCell>();
    //    DoFirstGenerationStep(activeCells);
    //    while (activeCells.Count > 0)
    //    {
    //        yield return delay;
    //        DoNextGenerationStep(activeCells);
    //    }
    //}

    public void Generate()
    {
        //WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            //yield return delay;
            DoNextGenerationStep(activeCells);
        }
        for (int i = 0; i < rooms.Count; i++)
        {
            CreateObstacleInRoom(rooms[i]);
            //rooms[i].Hide();
        }
    }

    private void CreateObstacleInRoom(MazeRoom room)
    {
        GameObject obstacle = mazeObstacles[Random.Range(0, mazeObstacles.Length)];
        MazeCell cell = room.RandomCell();
        room.obstacleCell = cell;
        obstacle = Instantiate(obstacle, this.transform);
        obstacle.transform.parent = cell.transform;
        obstacle.transform.localPosition = Vector3.zero;
        obstacle.transform.localScale = Vector3.one * 0.25f;
        room.obstacle = obstacle.GetComponent(typeof(Obstacle)) as Obstacle;
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        MazeCell newCell = CreateCell(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }

    private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab, this.transform) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab, this.transform) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (cell.room != otherCell.room)
        {
            MazeRoom roomToAssimilate = otherCell.room;
            cell.room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        //int currentIndex = 0;
        //int currentIndex = activeCells.Count/2;
        int currentIndex = Mathf.Max(activeCells.Count - 2, 0);
        //int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex)
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
                // No longer remove the cell here.
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
            // No longer remove the cell here.
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab, this.transform) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition =
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = null;
        if (cell.room.cells.Count > maxRoomSize)
        {
            prefab = doorPrefab;
        }
        else
        {
            prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        }
        MazePassage passage = Instantiate(prefab, this.transform) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab, this.transform) as MazePassage;
        if (passage is MazeDoor)
        {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else
        {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[Mathf.Max(0, Random.Range(-mazeWallMultiplier, wallPrefabs.Length))], this.transform) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefabs[Mathf.Max(0, Random.Range(-mazeWallMultiplier, wallPrefabs.Length))], this.transform) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public MazeRoom RandomRoom
    {
        get
        {
            MazeRoom r = rooms[Random.Range(0, rooms.Count)];
            while (r.cells.Count < 5)
            {
                r = rooms[Random.Range(0, rooms.Count)];
            }
            return r;
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }
}
