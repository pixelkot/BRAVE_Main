using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    public Text hudText;
    public FloatVariable obstaclesRemaining;

	// Use this for initialization
	public void SetHudText () {
        if (obstaclesRemaining.RuntimeValue > 0)
        {
            hudText.text = "Obstacles Remaining " + obstaclesRemaining.RuntimeValue;
        }
		else
        {
            hudText.text = "You Win! \n A portal has spawned in your room!";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
