using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSubTrigger : MonoBehaviour {

    public MazeDoor md;

    private void OnTriggerEnter(Collider other)
    {
        md.OnPlayerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        md.OnPlayerExit();
    }
}
