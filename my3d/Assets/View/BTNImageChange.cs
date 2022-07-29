using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTNImageChange : MonoBehaviour {

	private Sprite Defallsprit;
	private string Defalltext;
	public int num = 0;
    // Use this for initialization
    void Start () {
		 ///监听点击事件
        //transform.GetComponent<Button>().onClick.AddListener(OnClick);
        ///获取按钮初始默认图片
        Defallsprit = GameObject.Find("Canvas/Start/Image").GetComponent<Image>().sprite;
		Defalltext = GameObject.Find("Canvas/Start/Text").GetComponent<Text>().text;
	}
    
    // Update is called once per frame
    void Update () {
        
    }

    /// 按钮点击后所执行方法
    public void Click(Sprite MyFsprit)
    {
        //测试信息是否点击执行了
       //Debug.Log("click");

        //ischange = !ischange;
        if (num % 2 == 0)
        {
            ///更改按钮图片
            //transform.GetComponent<Image>().sprite = Mysprit;
			GameObject.Find("Canvas/Start/Image").GetComponent<Image>().sprite = MyFsprit;
			GameObject.Find("Canvas/Start/Text").GetComponent<Text>().text = "暂停";
			num++;
        }
        else
        {
            ///还原按钮图片
            GameObject.Find("Canvas/Start/Image").GetComponent<Image>().sprite = Defallsprit;
			GameObject.Find("Canvas/Start/Text").GetComponent<Text>().text = Defalltext;
			num++;
        }
    }
}
