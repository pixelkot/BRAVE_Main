/* Implementation of the Liar Obstacle
 	 Authors: Ahmet Oguzlu
 	 Updated: 03/01/2019 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Game States:
	-1 - hasn't started
	0 - ongoing
	1 - user won
	2 - user lost */

public class LiarObstacle : MonoBehaviour, Obstacle {
	private int state = -1;
	private string selection;

	public Transform prompt;
	public Transform yellowOrb;
	public Transform blackOrb;

	// Use this for initialization
	void Start () {
		state = 0;

		Transform text = Instantiate(prompt, this.transform);
		text.localPosition = new Vector3(0, 1, 0);

		Transform yOrb = Instantiate(yellowOrb, this.transform);
		yOrb.localPosition = new Vector3(1, 0.5f, 0);

		Transform bOrb = Instantiate(blackOrb, this.transform);
		bOrb.localPosition = new Vector3(-1, 0.5f, 0);

	}

	// Update is called once per frame
	void Update () {
		if (selection == "yellow") {
			state = 1;
		} else if (selection == "black") {
			state = 2;
		}
	}

	// Retruns the int corresponding to the current state of the game
	public int gameState() {
		return state;
	}

	// Setters
	public void setSelection(string str) {
		selection = str;
	}
}
