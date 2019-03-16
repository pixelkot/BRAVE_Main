using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public bool activated;
    public Vector3 up, down;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        up = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            transform.position = Vector3.Lerp(transform.position, up, Time.deltaTime * 5f);
        }
        if (activated)
        {
            transform.position = Vector3.Lerp(transform.position, down, Time.deltaTime * 5f);
        }
    }

    void OnVRTriggerDown()
    {
        activated = true;
    }
}
