using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);

        //Quaternion q = transform.localRotation;
       // transform.localRotation = new Quaternion(-q.x, q.y, -q.z, q.w);
    }
}
