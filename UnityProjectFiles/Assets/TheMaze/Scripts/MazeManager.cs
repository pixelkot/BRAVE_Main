using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

    public Maze mazePrefab;
    public RoomVariable currentRoom;

    public PlayerController playerPrefab;

    private PlayerController playerInstance;

    private Maze mazeInstance;

    private void Start()
    {
        BeginGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void BeginGame() 
    {
        mazeInstance = Instantiate(mazePrefab, this.transform) as Maze;
        //********* Extra Code For Testing ********************
        //Camera.allCameras[0].rect = new Rect(0f, 0f, 0.3f, 0.3f);
        //*****************************************************
        //StartCoroutine(mazeInstance.Generate());
        mazeInstance.Generate();
        //********* Extra Code For Testing ********************
        playerInstance = Instantiate(playerPrefab, this.transform) as PlayerController;
        MazeCell c = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
        c.room.Show();
        currentRoom.RuntimeValue = c.room;
        playerInstance.SetLocation(c);
        GameObject player = playerInstance.gameObject;
        mazeInstance.playerTransform = player.transform;
        Camera cam = player.GetComponentInChildren<Camera>();
        mazeInstance.playerCameraTransform = cam.transform;
        //*****************************************************
    }

    private void RestartGame() 
    {
        //StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        Destroy(playerInstance.gameObject);
        BeginGame();
    }
}
