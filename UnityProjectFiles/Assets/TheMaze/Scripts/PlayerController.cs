using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private MazeCell currentCell;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

    public void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            GameObject j = transform.Find("OVRCameraRig").gameObject;
            Transform T = Camera.main.transform;
            Vector3 newPosition = transform.localPosition + new Vector3(T.forward.x, 0, T.forward.z) * 0.05f;
            transform.localPosition = newPosition;
            j.transform.localPosition = new Vector3(0, 0, 0);
            //transform.localPosition = transform.localPosition + Vector3.up*0.01f;
        }
    }

}
