using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInitiator : MonoBehaviour {
	private bool init = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col) {
			init = true;
	}

	void OnCollisionExit(Collision col) {
			init = false;
	}

	public bool initiated() {
		return init;
	}
}
