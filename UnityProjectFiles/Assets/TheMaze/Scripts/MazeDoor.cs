using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : MazePassage
{
    public Transform hinge;
    public GameObject door;
    [Range(0f, 1f)]
    public float doorSpeed = 0.01f;
    public RoomVariable currentRoom;

    private IEnumerator closeCoroutine;
    private IEnumerator openCoroutine;
    private bool seePlayer = false;
    public bool locked = true;

    private MazeDoor OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
        }
    }

    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
    {
        base.Initialize(primary, other, direction);
        if (OtherSideOfDoor != null)
        {
            //hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != hinge)
            {
                child.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
            }
        }
    }

    public IEnumerator SlideOpen()
    {
        WaitForSeconds delay = new WaitForSeconds(0);
        while (door.transform.localPosition.y > -0.5)
        {
            //Debug.Log(transform.position.y);
            yield return delay;
            lower();
        }
    }

    public IEnumerator SlideClosed(bool left)
    {
        WaitForSeconds delay = new WaitForSeconds(0);
        while (door.transform.localPosition.y < 0.5)
        {
            //Debug.Log(transform.position.y);
            yield return delay;
            raise();
        }
        if (left)
        {
            cell.room.Hide();
        }
    }

    public void lower()
    {
        door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(door.transform.localPosition.x, -0.51f, door.transform.localPosition.z), doorSpeed);
    }

    public void raise()
    {
        door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(door.transform.localPosition.x, 0.51f, door.transform.localPosition.z), doorSpeed);
    }

    public void HaltCoroutines()
    {
        if (openCoroutine != null)
        {
            StopCoroutine(openCoroutine);
        }
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }
    }

    public void OpenDoor()
    {
        HaltCoroutines();
        openCoroutine = SlideOpen();
        StartCoroutine(openCoroutine);
    }

    public void CloseDoor(bool left)
    {
        HaltCoroutines();
        closeCoroutine = SlideClosed(left);
        StartCoroutine(closeCoroutine);
    }

    public void ScreenLock()
    {

    }

    public void Unlock()
    {
        locked = false;
        OtherSideOfDoor.locked = false;
    }

    public void Lock()
    {
        locked = true;
        OtherSideOfDoor.locked = true;
    }

    public void OnPlayerEnter()
    {
        seePlayer = true;
        if (!locked)
        {
            OtherSideOfDoor.cell.room.Show();
            OpenDoor();
            OtherSideOfDoor.OpenDoor();
        }
    }

    public void OnPlayerExit()
    {
        seePlayer = false;
        if (!locked)
        {
            if (!OtherSideOfDoor.seePlayer)
            {
                CloseDoor(false);
                OtherSideOfDoor.CloseDoor(true);
                currentRoom.RuntimeValue = cell.room;
            }
        }
    }
}
