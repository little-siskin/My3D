using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPloughChildPanels : MonoBehaviour {
	public GameObject[] myPanel;
	public GameObject currentPanel;
	// Use this for initialization
	void Start () {
		if(myPanel.Length <= 0){
			return;
		}
		for(int i = 0; i < myPanel.Length; i++){
			myPanel[i].gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Click(GameObject page){
		if (page == null)
        {
            return;
        }
		page.gameObject.SetActive(true);
	}

	public void Exit(GameObject page){
		page.gameObject.SetActive(false);
	}
}
