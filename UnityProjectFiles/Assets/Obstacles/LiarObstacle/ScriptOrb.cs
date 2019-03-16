using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOrb : MonoBehaviour {
	private bool selected = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col) {
			selected = true;
	}

	// Returns if THIS orb is selected or not
	public bool isSelected() {
		return selected;
	}

	public void setSelected(bool b) {
		selected = b;
	}
}
