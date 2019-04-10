using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public OVRInput.Controller controller;
    public Transform playerTransform;
    public RoomVariable currentRoom;

    //private int layerMask = 1 << 8;
    private int layerMask = ~(1 << 9);
    private float indexTriggerState = 0;
    private float oldIndexTriggerState = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        oldIndexTriggerState = indexTriggerState;
        indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if (indexTriggerState > 0.9f && oldIndexTriggerState < 0.9f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.gameObject.tag == "floor")
                {
                    if (hit.transform.parent.Find("LiarObstacle(Clone)") == null || currentRoom.RuntimeValue.completed)
                    {
                        Transform tile = hit.transform;
                        playerTransform.position = new Vector3(tile.position.x, playerTransform.position.y, tile.position.z);
                        currentRoom.RuntimeValue = hit.transform.parent.gameObject.GetComponent<MazeCell>().room;
                    }
                }
                else
                {
                    Debug.Log("Object: " + hit.transform.gameObject.name);
                }
            }
        }
	}
}
