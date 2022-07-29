using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//sing EventSystems;
using UnityEngine.EventSystems;

public class TheThirdPersonCamera : MonoBehaviour {
	//private EventSystem myEventSystem;
	public bool isPressLeft = false;//是否刚刚开始旋转
	public float distanceAway=1.7f;			
	public float distanceUp=1.3f;			
	public float smooth=2f;
	public float ScaleSpeed = 0.5f;
	private Vector3 m_TargetPosition;		// the position the camera is trying to be in)
	public Vector2 newPosition;
    public Vector2 oldPosition;
	Transform follow;        //the position of Tractor
	// Use this for initialization
	void Start () {
		follow = GameObject.Find ("tractor").transform;	
		transform.LookAt(follow);
		//myEventSystem = GetComponent<EventSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Zoom out
		if (Input.GetAxis("Mouse ScrollWheel") <0)
		{
			//Camera.main.transform.Translate(0,0,-1*ScaleSpeed);
			distanceAway = distanceAway + 1*ScaleSpeed;
		}
		//Zoom in
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			//Camera.main.transform.Translate(0,0,1*ScaleSpeed);
			distanceAway = distanceAway - 1*ScaleSpeed;
		}

				// setting the target position to be the correct offset from the 
		m_TargetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
		
		// making a smooth transition between it's current position and the position it wants to be in
		transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
		
		// make sure the camera is looking the right way!
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())//鼠标左键
        {
			
				if (isPressLeft == false)
				{
					oldPosition = Input.mousePosition;
					isPressLeft = true;
					return;
				}
				else
				{
					newPosition = Input.mousePosition;
				}
				if (Mathf.Abs(newPosition.x - oldPosition.x) > Mathf.Abs(newPosition.y - oldPosition.y) && Mathf.Abs(newPosition.x - oldPosition.x) > 0.5f)
				{
					transform.Rotate(Vector3.down * (oldPosition.x - newPosition.x) * Time.deltaTime * 5, Space.World);//绕Y轴进行旋转//
				}
				//控制x轴，即上下角度旋转
				if (Mathf.Abs(newPosition.x - oldPosition.x) < Mathf.Abs(newPosition.y - oldPosition.y) && Mathf.Abs(newPosition.y - oldPosition.y) > 0.5f)
				{
					Vector3 add = transform.localEulerAngles - Vector3.right * (newPosition.y - oldPosition.y) * 0.1f;
					float xx = add.x;
					if (xx >= 66 && xx < 180)
					{
						xx = 66;
					}
					if (xx < 0)
					{
					xx = 0;
					}
					transform.localEulerAngles = new Vector3(xx,transform.localEulerAngles.y, transform.localEulerAngles.z);
				}
				oldPosition = newPosition;
			
      	}else{
			transform.LookAt(follow);
	  }
	}
	public void ZoomOut(){
		distanceAway = distanceAway + 1*ScaleSpeed;
		// setting the target position to be the correct offset from the 
		m_TargetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
		
		// making a smooth transition between it's current position and the position it wants to be in
		transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
	}
	public void ZoomIn(){
		distanceAway = distanceAway - 1*ScaleSpeed;
		// setting the target position to be the correct offset from the 
		m_TargetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
		
		// making a smooth transition between it's current position and the position it wants to be in
		transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
	}
}
