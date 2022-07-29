using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Data;
using System.Linq;
using System.Text;

//using System.Threading.Tasks;

public class myTractor : MonoBehaviour {

	List<double> x_list = new List<double>();
    List<double> y_list = new List<double>();
	List<double> z_list = new List<double>();
	int i = 0;
	double x = 0;
	double y = 0;
	double z = 0;

	//由于unity中坐标系为左手坐标系，Y轴向上，X轴向左，Z轴向右
	//实际中Z向上，X向左，Y向右，所以这里读取位置时需要将Y与Z调换

	void Awake() {
		string filePath = "/home/vertin/UnityProject/my3d/garage_abzhuan.csv";
		System.Text.Encoding encoding = Encoding.Default;
		System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
                    System.IO.FileAccess.Read);
        System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);
		string strLine = "";
        //记录每行记录中的各字段内容
        string[] aryLine = null;
		bool IsFirst = true;
		bool IsSecond = true;
		double orx = 0;
		double ory = 0;
		double orz = 0;
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
				orx = double.Parse(aryLine[0]);
				ory = double.Parse(aryLine[2]);
				orz = double.Parse(aryLine[1]);
			}
		}
		sr.Close();
        fs.Close();
	}

	// Use this for initialization
	void Start () {
		// x = x_list[2];
		// y = y_list[2];
		// z = z_list[2];
		// //transform.LookAt(new Vector3 ((float)x,(float)y,(float)z));
		// transform.rotation = Quaternion.LookRotation(new Vector3 ((float)x,(float)y,(float)z));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		try{
			x = x_list[i];
			y = y_list[i];
			z = z_list[i];
			transform.Translate(new Vector3 ((float)x,(float)y,(float)z) * Time.deltaTime, Space.World);
			//transform.Translate(new Vector3 ((float)x,(float)y,(float)z) *, Space.World);
			if(i != 0)
			//transform.Translate(new Vector3 ((float)x,(float)y,(float)z), Space.World);
				transform.rotation = Quaternion.LookRotation(new Vector3 ((float)x,(float)y,(float)z));
			i++;
		}catch(Exception ex)
		{
			print(ex.ToString());
			Time.timeScale = 0;
		}
	}
}
