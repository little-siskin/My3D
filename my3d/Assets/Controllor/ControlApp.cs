using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlApp : MonoBehaviour {
	public int num = 0;
	void Awake(){
		Time.timeScale = 0;
	}

	public void OnStart(){
		Time.timeScale = 1;
	}
	public void OnPause(){
		Time.timeScale = 0;
	}
	
	public void Control(){
		if(num%2 == 0){
			OnStart();
			num++;
		}else{
			OnPause();
			num++;
		}
	}
}
