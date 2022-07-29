using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour {
	private Sprite Defallsprit;
	private string Defalltext;
	public int num = 0;
	//private GameObject myLockText = root.transform.Find("LockText").gameObject;
	// Use this for initialization
	void Start () {
		//myLockText = this.GetComponent<LockText>();
		Defallsprit = GameObject.Find("Canvas/Lock/Image").GetComponent<Image>().sprite;
		Defalltext = GameObject.Find("Canvas/Lock/Text").GetComponent<Text>().text;
	}
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	public void Click(Sprite MyFsprit)
	{
		if (num % 2 == 0)
        {
            ///更改按钮图片
            //transform.GetComponent<Image>().sprite = Mysprit;
			GameObject.Find("Canvas/Lock/Image").GetComponent<Image>().sprite = MyFsprit;
			GameObject.Find("Canvas/Lock/Text").GetComponent<Text>().text = "屏幕已锁定";
			//禁用button构件
			GameObject.Find("Canvas/Start").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/End").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/About").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Setting").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Plough").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Guideline").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Other").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Enlarge").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Narrow").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/PointA").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/PointB").GetComponent<Button>().enabled = false;
			num++;
        }
        else
        {
            ///还原按钮图片
            GameObject.Find("Canvas/Lock/Image").GetComponent<Image>().sprite = Defallsprit;
			GameObject.Find("Canvas/Lock/Text").GetComponent<Text>().text = Defalltext;
			//启用button构件
			GameObject.Find("Canvas/Start").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/End").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/About").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/Setting").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/Plough").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/Guideline").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/Other").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/Enlarge").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/Narrow").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/PointA").GetComponent<Button>().enabled = true;
			GameObject.Find("Canvas/PointB").GetComponent<Button>().enabled = true;
			num++;

        }
	}
}
