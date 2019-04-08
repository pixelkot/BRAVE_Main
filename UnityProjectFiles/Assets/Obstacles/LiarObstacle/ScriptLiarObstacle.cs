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

public class ScriptLiarObstacle : MonoBehaviour, Obstacle {
	private int state = -1;
	private ScriptOrb yellowScript;
	private ScriptOrb blackScript;
	private ScriptInitiator initScript;
	private Transform text;
	private Transform yOrb;
	private Transform bOrb;
	private Transform init;

	public Transform initiator;
	public Transform prompt;
	public Transform yellowOrb;
	public Transform blackOrb;
	public Transform winMessage;
	public Transform loseMessage;
    public Transform completed;
    public GameEvent OnObstacleCompleted;
    public FloatVariable ObstaclesCompleted;

	// Use this for initialization
	void Start () {

		init = Instantiate(initiator, this.transform);
		init.localPosition = new Vector3(0, 1, 0);

		initScript = init.gameObject.GetComponent<ScriptInitiator>();

	}

	// Update is called once per frame
	void Update () {

		if (state == -1) {
			if (initScript.initiated()) {
				Destroy(init.gameObject);
				state = 0;

				beginObstacle();
			}
		}


		if (state == 0) {
			if (yellowScript.isSelected()) {
				state = 1;
				yellowScript.setSelected(false);
			} else if (blackScript.isSelected()) {
				state = 2;
				blackScript.setSelected(false);
			}
		}


		if (state == 1 || state == 2) {
			endObstacle(state);
		}
	}

	// Retruns the int corresponding to the current state of the game
	public int gameState() {
		return state;
	}

	// Begins the obstacle
	private void beginObstacle() {
		text = Instantiate(prompt, this.transform);
		text.localPosition = new Vector3(0, 2, 0);


		yOrb = Instantiate(yellowOrb, this.transform);
		yOrb.localPosition = new Vector3(1, 0.6f, 0);

		bOrb = Instantiate(blackOrb, this.transform);
		bOrb.localPosition = new Vector3(-1, 0.6f, 0);

		yellowScript = yOrb.gameObject.GetComponent<ScriptOrb>();
		blackScript = bOrb.gameObject.GetComponent<ScriptOrb>();
	}

	// Ends the obstacle based on if the user won/lost
	private void endObstacle(int outcome) {
		if (text != null) {
			Destroy(text.gameObject);
		} if (yOrb != null) {
			Destroy(yOrb.gameObject);
		} if (bOrb != null) {
			Destroy(bOrb.gameObject);
		}

		if (outcome == 1) {
            ObstaclesCompleted.RuntimeValue += 1;
            Debug.Log("Number of Obstacles Completed " + ObstaclesCompleted.RuntimeValue);
            OnObstacleCompleted.Raise();
            Transform win = Instantiate(winMessage, this.transform);
			win.localPosition = new Vector3(0, 2, 0);

			Destroy(win.gameObject, 5f);

            Transform end = Instantiate(completed, this.transform);
            end.localPosition = new Vector3(0, 1, 0);
            Destroy(this);
		}

		if (outcome == 2) {
			Transform loss = Instantiate(loseMessage, this.transform);
			loss.localPosition = new Vector3(0, 2, 0);

			Destroy(loss.gameObject, 5f);

			Invoke("beginObstacle", 6f);
			state = 0;
		}

	}
}
