using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFrame : MonoBehaviour {

	public int targetFrameRate = 90;
	// Use this for initialization
	void Awake () {
		Application.targetFrameRate = targetFrameRate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
