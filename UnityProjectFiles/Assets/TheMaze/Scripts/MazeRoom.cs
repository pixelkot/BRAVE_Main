using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : ScriptableObject
{
    public Obstacle obstacle;
    public int settingsIndex;
    public MazeCell obstacleCell;

    public MazeRoomSettings settings;

    public List<MazeCell> cells = new List<MazeCell>();

    public bool completed = false;

    public void Add(MazeCell cell)
    {
        cell.room = this;
        cells.Add(cell);
    }

    public void Assimilate(MazeRoom room)
    {
        for (int i = 0; i < room.cells.Count; i++)
        {
            Add(room.cells[i]);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Hide();
        }
    }

    public void Show()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Show();
        }
    }

    public void Lock()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Lock();
        }
    }

    public void Unlock()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Unlock();
        }
        completed = true;
    }

    public MazeCell RandomCell()
    {
        MazeCell c = cells[Random.Range(0, cells.Count)];
        return c;

    }

    public MazeCell RandomUnfilledCell()
    {
        MazeCell c = cells[Random.Range(0, cells.Count)];
        while (c == obstacleCell) {
            c = cells[Random.Range(0, cells.Count)];
            if (cells.Count == 1)
            {
                break;
            }
        }
        return c;
    }
}