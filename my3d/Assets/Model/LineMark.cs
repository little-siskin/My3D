using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMark : MonoBehaviour {
 
	private LineRenderer line;  
	private int i;  
	//public GameObject obs;  
	public GameObject run;  
	Vector3 RunStart;
	Vector3 RunNext;

	// Use this for initialization
	void Start () {
		RunStart = run.transform.position;
		//clone = (GameObject)Instantiate(obs, run.transform.position, run.transform.rotation);//克隆一个带有LineRender的物体   
		line = GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
		//line.SetColors(Color.red, Color.red);//设置颜色  
		line.startColor = Color.red;
		line.endColor = Color.red;
		//line.SetWidth(1f, 1f);//设置宽度  
		i = 0;
	}

	// Update is called once per frame  
	void FixedUpdate () {  

		RunNext = run.transform.position;

		if (RunStart != RunNext) {
			i++;
			line.SetVertexCount(i);//设置顶点数 
			//line.numPositions(i);
			line.SetPosition(i-1, run.transform.position);
			
		}

		RunStart = RunNext;




	}  
}
