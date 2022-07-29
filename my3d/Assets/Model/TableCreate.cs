using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class TableCreate : MonoBehaviour
{
	public GameObject Row_Prefab;//表头预设
	public GameObject table;
	public GameObject NewInputText;
	public Toggle NewTog;
	public Transform father;
	public GameObject NewEditInputText;
	public Toggle NewEditTog;
	//public RectTransform  DelFather;
	//public Transform father;
	int robNumber = 0;
	int clickUpTime = 0;
	int clickDownTime = 0;
	int currentNumber = 0;
	List<GameObject> rows = new List<GameObject>();
	private String str;
	int totalNumber;
	//List<GameObject> rows;
	void Start()
	{
		String str1 = System.IO.Directory.GetCurrentDirectory();
		String str2 = "/GuideLine.txt";
		str = String.Format("{0}{1}",str1,str2);
		Read(str);
	}
	void Update(){
		//Read(str);
	}
	public void Read(String path){
		// if(Directory.Exists(path) == false){
		// 	File.Create(path);
		// }
		StreamReader sr = new StreamReader(path);
		String line;
		String[] aryLine = null;
		int i = 0;
		while((line = sr.ReadLine()) != null)
		{
			//Debug.Log(line.ToString());
			aryLine = line.Split(' ');
			//在Table下创建新的预设实例
			GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
			row.name = "row" + (i++);
			row.transform.SetParent(table.transform);
			row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
			//设置预设实例中的各个子物体的文本内容
			row.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
			row.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
			row.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
			//插入
			rows.Add(row);
			//Console.WriteLine(line.ToString());
			robNumber++;
		}
	}
	public void refresh(){
		String[] lines = File.ReadAllLines(str);
		String specificLine = lines[father.childCount - 1];
		String[] aryLine = null;
		aryLine = specificLine.Split(' ');
		GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
		row.name = "row" + (father.childCount - 1);
		row.transform.SetParent(table.transform);
		row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
		//设置预设实例中的各个子物体的文本内容
		row.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
		row.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
		row.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
		rows.Add(row);
	}
	public void ClickUp(){
		totalNumber = rows.Count;
		if(clickUpTime == 0 && clickDownTime == 0){
			currentNumber = totalNumber;
			currentNumber--;
			GameObject obj = rows[currentNumber % totalNumber];
			obj.GetComponent<Image>().color = Color.green;
			clickUpTime++;
		}else{
			if(currentNumber == 0){
				currentNumber = totalNumber - 1;
			}else{
				currentNumber--;
			}
			GameObject obj = rows[currentNumber % totalNumber];
			GameObject objLater = rows[(currentNumber + 1) % totalNumber];
			obj.GetComponent<Image>().color = Color.green;
			objLater.GetComponent<Image>().color = Color.white;
			clickUpTime++;
		}
		//Debug.Log(currentNumber);
	}
	public void ClickDown(){
		totalNumber = rows.Count;
		if(clickUpTime == 0 && clickDownTime == 0){
			currentNumber = -1;
			currentNumber++;
			GameObject obj = rows[currentNumber];
			obj.GetComponent<Image>().color = Color.green;
			clickDownTime++;
		}else{
			currentNumber++;
			GameObject obj = rows[currentNumber % totalNumber];
			GameObject objBefore = rows[(currentNumber - 1) % totalNumber];
			obj.GetComponent<Image>().color = Color.green;
			objBefore.GetComponent<Image>().color = Color.white;
			clickDownTime++;
		}
	}
	public void NewInfoGuideLine(){
		String Kind;
		String Time = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
		String NewName = NewInputText.GetComponent<InputField>().text;
		using (FileStream fs = new FileStream(@str, FileMode.Append, FileAccess.Write)){
			//fs.Lock(0, fs.Length);
			StreamWriter sw = new StreamWriter(fs);
			if(NewTog.isOn == true){
				Kind = "AB类型";
			}else{
				Kind = "其他类型";
			}
			//DateTime.Now.ToString();
			sw.WriteLine(NewName + " " + Kind + " " + Time);
            sw.Flush();
			sw.Close();
		}
	}
	public void EditInfoGuideLine(){
		List<String> allLine = new List<String>();
		String Kind;
		String Time = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
		String NewName = NewEditInputText.GetComponent<InputField>().text;
		StreamReader sr = new StreamReader(str);
		String line;
		//String[] allLine = null;
		String[] aryLine = null;
		//int i = 0;
		if((clickUpTime != 0) || (clickDownTime != 0)){
			while((line = sr.ReadLine()) != null){
				allLine.Add(line);
			}
			if(NewEditTog.isOn == true){
				Kind = "AB类型";
			}else{
				Kind = "其他类型";
			}
			//aryLine
			if(NewName == ""){
				aryLine = allLine[currentNumber%totalNumber].Split(' ');
				NewName = aryLine[0];
			}
			allLine[currentNumber%totalNumber] = (NewName + " " + Kind + " " + Time);
			Debug.Log(NewName + " " + Kind + " " + Time);
			File.Delete(str);
			FileStream fs = new FileStream(str, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			for(int i = 0;i < allLine.Count;i++){
				sw.WriteLine(allLine[i]);//开始写入值
			}
			sw.Close();
			fs.Close();
			GameObject row = rows[currentNumber%totalNumber];
			aryLine = allLine[currentNumber%totalNumber].Split(' ');
			row.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
			row.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
			row.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
			//sw.WriteLine(Log);//开始写入值
		}
	}
	public void DelInfoGuideLine(){
		List<String> allLine = new List<String>();
		StreamReader sr = new StreamReader(str);
		String line;
		if((clickUpTime != 0) || (clickDownTime != 0)){
			while((line = sr.ReadLine()) != null){
				allLine.Add(line);
			}
			allLine.Remove(allLine[currentNumber%totalNumber]);
			File.Delete(str);
			FileStream fs = new FileStream(str, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			for(int a = 0;a < allLine.Count;a++){
				sw.WriteLine(allLine[a]);//开始写入值
			}
			sw.Close();
			fs.Close();
			for (int childIndex = father.childCount-1; childIndex >=0; childIndex--)
			{
				if (father.GetChild(childIndex).gameObject.name != "1")
				{
					GameObject.DestroyImmediate(father.GetChild(childIndex).gameObject);
				}
			
			}
			rows.Clear();
			//father.childCount--;
			Read(str);
			currentNumber = 0;
			clickUpTime = 0;
			clickDownTime = 0;
		}
	}
}