using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using System.Text;
public class OriginalLine : MonoBehaviour {
	List<double> x_list = new List<double>();
    List<double> y_list = new List<double>();
	List<double> z_list = new List<double>();
	private LineRenderer lr;
	double x = 0;
	double y = 0;
	double z = 0;
	void Awake(){
		string filePath = "/home/vertin/UnityProject/my3d/garage_abzhuan.csv";
		System.Text.Encoding encoding = Encoding.Default;
		System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
                    System.IO.FileAccess.Read);
        System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);
		lr = GetComponent<LineRenderer>();
		string strLine = "";
        //记录每行记录中的各字段内容
        string[] aryLine = null;
		bool IsFirst = true;
		bool IsSecond = true;
		double orx = 0;
		double ory = 0;
		double orz = 0;
		//lr.startColor = Color.green;                               //设置画线开始颜色
        //lr.endColor = Color.green;                                 //设置画线结束颜色
		while ((strLine = sr.ReadLine()) != null)
		{
			if ((IsFirst == true)&&(IsSecond == true))
			{
				IsFirst = false;
				//print(orx);
			}
			else if((IsFirst == false)&&(IsSecond == true))
			{
				aryLine = strLine.Split(',');
				x_list.Add(0);
				y_list.Add(0);
				z_list.Add(0);
				orx = double.Parse(aryLine[0]);
				ory = double.Parse(aryLine[2]);
				orz = double.Parse(aryLine[1]);
				IsSecond = false;
			}
			else
			{
				aryLine = strLine.Split(',');
				x_list.Add(double.Parse(aryLine[0])-orx);
				y_list.Add(double.Parse(aryLine[2])-ory);
				z_list.Add(double.Parse(aryLine[1])-orz);
				// orx = double.Parse(aryLine[0]);
				// ory = double.Parse(aryLine[2]);
				// orz = double.Parse(aryLine[1]);
			}
		}
		sr.Close();
        fs.Close();
		Vector3[] positions = new Vector3[x_list.Count];
		//print(positions.Length);
		for (int i = 0; i < x_list.Count; i++)
        {
            positions[i] = new Vector3 ((float)x,(float)y,(float)z);
			//print(Time.deltaTime);
            x = x_list[i] * 0.05;
            y = y_list[i] * 0.05;
            z = z_list[i] * 0.05;
            //positions[1] = new vector3(0.0f, 2.0f, 0.0f);
            //positions[2] = new vector3(2.0f, -2.0f, 0.0f);
            //positions[3] = new vector3(2.0f, -4.0f, 0.0f); 
            //Debug.Log(positions[i]);
        }
		lr.positionCount = positions.Length;
        lr.SetPositions(positions);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
