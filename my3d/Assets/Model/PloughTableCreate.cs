using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class PloughTableCreate : MonoBehaviour {
	public GameObject Row_Prefab;//表头预设
	public GameObject table;
	public GameObject NewNameInputText;
	public GameObject NewWidthInputText;
	public GameObject NewDistanceInputText;
	public GameObject EditNameInputText;
	public GameObject EditWidthInputText;
	public GameObject EditDistanceInputText;
	//public RectTransform  DelFather;
	// public Toggle NewTog;
	public Transform father;
	int rowbNumber = 0;
	int clickUpTime = 0;
	int clickDownTime = 0;
	int currentNumber = 0;
	List<GameObject> rowbs = new List<GameObject>();
	private String str;
	int totalNumber;
	// Use this for initialization
	void Start () {
		String str1 = System.IO.Directory.GetCurrentDirectory();
		String str2 = "/Plough.txt";
		str = String.Format("{0}{1}",str1,str2);
		Read(str);
	}
	
	// Update is called once per frame
	void Update () {

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
			GameObject rowb = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
			//GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.localPosition, table.transform.localRotation) as GameObject;
			rowb.name = "rowb" + (i++);
			rowb.transform.SetParent(table.transform);
			rowb.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
			//设置预设实例中的各个子物体的文本内容
			rowb.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
			rowb.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
			rowb.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
			rowb.transform.Find("Cell3").GetComponent<Text>().text = aryLine[3];
			//Console.WriteLine(line.ToString());
			rowbs.Add(rowb);
			rowbNumber++;
		}
	}
	public void ClickUp(){
		totalNumber = rowbs.Count;
		if(clickUpTime == 0 && clickDownTime == 0){
			currentNumber = totalNumber;
			currentNumber--;
			GameObject obj = rowbs[currentNumber % totalNumber];
			obj.GetComponent<Image>().color = Color.green;
			clickUpTime++;
			//currentNumber--;
		}else{
			// GameObject obj = rowbs[currentNumber % totalNumber];
			// GameObject objLater = rowbs[(currentNumber + 1) % totalNumber];
			if(currentNumber == 0){
				currentNumber = totalNumber - 1;
			}else{
				currentNumber--;
			}
			GameObject obj = rowbs[currentNumber % totalNumber];
			GameObject objLater = rowbs[(currentNumber + 1) % totalNumber];
			obj.GetComponent<Image>().color = Color.green;
			objLater.GetComponent<Image>().color = Color.white;
			clickUpTime++;
		}
	}
	public void ClickDown(){
		totalNumber = rowbs.Count;
		if(clickUpTime == 0 && clickDownTime == 0){
			currentNumber = -1;
			currentNumber++;
			//GameObject obj = rowbs[currentNumber % totalNumber];
			GameObject obj = rowbs[currentNumber];
			obj.GetComponent<Image>().color = Color.green;
			clickDownTime++;
		}else{
			//if(currentNumber != totalNumber-1){
				currentNumber++;
			//}
			// GameObject obj = rowbs[currentNumber % totalNumber];
			// GameObject objBefore = rowbs[(currentNumber - 1) % totalNumber];
			GameObject obj = rowbs[currentNumber % totalNumber];
			GameObject objBefore = rowbs[(currentNumber - 1) % totalNumber];
			obj.GetComponent<Image>().color = Color.green;
			objBefore.GetComponent<Image>().color = Color.white;
			clickDownTime++;
		}

	}
	public void refresh(){
		String NewName = NewNameInputText.GetComponent<InputField>().text;
		String NewWidth = NewWidthInputText.GetComponent<InputField>().text;
		String NewDistance = NewDistanceInputText.GetComponent<InputField>().text;
		//if((NewName != "")||(NewWidth != "")||(NewDistance != "")){
		String[] lines = File.ReadAllLines(str);
		Debug.Log(father.childCount);
		String specificLine = lines[father.childCount - 1];
		String[] aryLine = null;
		aryLine = specificLine.Split(' ');
		GameObject rowb = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
		rowb.name = "rowb" + (father.childCount - 1);
		rowb.transform.SetParent(table.transform);
		rowb.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
		//设置预设实例中的各个子物体的文本内容
		// rowb.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
		// rowb.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
		// rowb.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
		rowb.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
		rowb.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
		rowb.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
		rowb.transform.Find("Cell3").GetComponent<Text>().text = aryLine[3];
		rowbs.Add(rowb);
		//}
	}
	public void NewInfoPlough(){
		String Kind;
		//String Time = DateTime.Now.ToString(); 
		String Time = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
		String NewName = NewNameInputText.GetComponent<InputField>().text;
		String NewWidth = NewWidthInputText.GetComponent<InputField>().text;
		String NewDistance = NewDistanceInputText.GetComponent<InputField>().text;
		using (FileStream fs = new FileStream(@str, FileMode.Append, FileAccess.Write))
		{
			//fs.Lock(0, fs.Length);
			StreamWriter sw = new StreamWriter(fs);
			// if(NewTog.isOn == true){
			// 	Kind = "AB类型";
			// }else{
			// 	Kind = "其他类型";
			// }
			// //DateTime.Now.ToString(); 
			sw.WriteLine(NewName + " " + NewWidth + " " + Time + " " + NewDistance);
			//fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。
            sw.Flush();
			sw.Close();
		}
	}
	public void EditInfoPlough(){
		List<String> allLine = new List<String>();
		String Kind;
		String Time = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
		String EditName = EditNameInputText.GetComponent<InputField>().text;
		String EditWidth = EditWidthInputText.GetComponent<InputField>().text;
		String EditDistance = EditDistanceInputText.GetComponent<InputField>().text;
		StreamReader sr = new StreamReader(str);
		String line;
		//String[] allLine = null;
		String[] aryLine = null;
		//int i = 0;
		if((clickUpTime != 0) || (clickDownTime != 0)){
			while((line = sr.ReadLine()) != null){
				allLine.Add(line);
			}
			//aryLine
			if(EditName == ""){
				aryLine = allLine[currentNumber%totalNumber].Split(' ');
				EditName = aryLine[0];
			}
			if(EditWidth == ""){
				aryLine = allLine[currentNumber%totalNumber].Split(' ');
				EditWidth = aryLine[1];
			}
			if(EditDistance == ""){
				aryLine = allLine[currentNumber%totalNumber].Split(' ');
				EditDistance = aryLine[3];
			}
			allLine[currentNumber%totalNumber] = (EditName + " " + EditWidth + " " + Time + " " + EditDistance);
			//Debug.Log(NewName + " " + Kind + " " + Time);
			File.Delete(str);
			FileStream fs = new FileStream(str, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			for(int i = 0;i < allLine.Count;i++){
				sw.WriteLine(allLine[i]);//开始写入值
			}
			sw.Close();
			fs.Close();
			GameObject rowb = rowbs[currentNumber%totalNumber];
			aryLine = allLine[currentNumber%totalNumber].Split(' ');
			rowb.transform.Find("Cell0").GetComponent<Text>().text = aryLine[0];
			rowb.transform.Find("Cell1").GetComponent<Text>().text = aryLine[1];
			rowb.transform.Find("Cell2").GetComponent<Text>().text = aryLine[2];
			rowb.transform.Find("Cell3").GetComponent<Text>().text = aryLine[3];
			//sw.WriteLine(Log);//开始写入值
		}
	}
	public void DelInfoPlough(){
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
			//totalNumber = father.childCount-1;
			//father.childCount--;
			rowbs.Clear();
			Read(str);
			currentNumber = 0;
			clickUpTime = 0;
			clickDownTime = 0;
		}
	}
}
