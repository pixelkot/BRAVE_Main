using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public AudioClip WindClip;
    public AudioSource WindSource;
	// Use this for initialization
	void Start () {
		WindSource.clip = WindClip;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            WindSource.Play();
        }
	}
}
