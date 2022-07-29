using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePointAB : MonoBehaviour {
	private GameObject A;
	private GameObject B;
	public GameObject Tractor;
	// Use this for initialization
	void Start () {
		A = GameObject.Find("CapsuleA");
		B = GameObject.Find("CapsuleB");
		A.gameObject.SetActive (false); 
		B.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	public void ClickA(){
		A.gameObject.SetActive (true); 
		A.transform.localPosition = Tractor.transform.localPosition;
		GameObject.Find("Canvas/PointA").GetComponent<Button>().interactable = false;
	}
	public void ClickB(){
		B.gameObject.SetActive (true);
		B.transform.localPosition = Tractor.transform.localPosition;
		GameObject.Find("Canvas/PointB").GetComponent<Button>().interactable = false;
	}
}
